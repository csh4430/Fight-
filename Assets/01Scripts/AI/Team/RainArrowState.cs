using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolBox.Pools;

public class RainArrowState : AIState, ISkillState
{
    [field: SerializeField]
    public float CoolDown { get; set; }
    private float _coolDown = 0;

    [field: SerializeField]
    public bool IsUsingSkill { get; set; } = false;

    [field: SerializeField]
    public Slider CoolDownSilder { get; set; }
    public override Action OnStateAction { get; set; }

    [field: SerializeField]
    public override List<AITransition> Transition { get; set; }
    [field : SerializeField]
    public AudioClip skillClip { get; set; }

    public GameObject Circle;
    public float Range;

    [SerializeField]
    private Transform _basePos;
    private AgentAnimation _anim;
    private AgentAudio _audio;

    [SerializeField]
    private GameObject arrowLauncher = null;


    private void Awake()
    {
        _anim = _basePos.GetComponent<AgentAnimation>();
        _audio = _basePos.GetComponent<AgentAudio>();
        OnStateAction += () =>
        {
            if (IsUsingSkill)
                return;
            if (_coolDown > 0)
                return;
            IsUsingSkill = true;
            StartCoroutine(Check());
        };
    }

    private IEnumerator Check()
    {
        _anim.PlaySpecialAnimation();
        _audio.SetAudioVolume(0.1f);
        _audio.PlayClipSound(skillClip);
        yield return new WaitForSeconds(0.4f);
        RaycastHit hit;
        GameObject circle = Circle.Reuse();
        circle.GetComponent<Radius>().SetRadius(Range);
        Vector3 targetPos;
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Floor"));
            if (hit.collider != null)
            {
                targetPos = new Vector3(hit.point.x, _basePos.position.y, hit.point.z);
                _basePos.LookAt(targetPos);
                circle.transform.position = hit.point;
                if (Input.GetMouseButton(0))
                {
                    IsUsingSkill = false;
                    _coolDown = CoolDown;
                    circle.Release();
                    GameObject launcher = arrowLauncher.Reuse(hit.point + Vector3.up * 10, Quaternion.identity);
                    launcher.GetComponent<SkyArrowLauncher>().Summon(Range);
                    launcher.Release();
                    _anim.RePlay();
                    Collider[] victims = Physics.OverlapSphere(hit.point, Range, LayerMask.GetMask("Enemy"));
                    foreach(Collider c in victims)
                    {
                        IHittable iHit = c.GetComponent<IHittable>();
                        iHit.DamageAgent(5, _basePos.gameObject);
                    }
                    yield break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update()
    {
        if (_coolDown > 0)
        {
            _coolDown -= Time.deltaTime;
            if (_coolDown < 0)
                _coolDown = 0;
            CoolDownSilder.value = _coolDown / CoolDown;
        }
    }
}
