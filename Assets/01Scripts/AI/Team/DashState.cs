using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashState : AIState, ISkillState
{
    public override Action OnStateAction { get; set; }

    [field: SerializeField]
    public override List<AITransition> Transition { get; set; }

    public Transform _basePos = null;

    [field: SerializeField]
    public float CoolDown { get; set; }
    private float _coolDown = 0;

    [field: SerializeField]
    public bool IsUsingSkill { get; set; } = false;

    [field: SerializeField]
    public Slider CoolDownSilder { get; set; }

    private AgentAnimation _anim = null;
    private AgentMove _move = null;
    private void Awake()
    {
        _anim = _basePos.GetComponent<AgentAnimation>();
        _move = _basePos.GetComponent<AgentMove>();
        OnStateAction += () =>
        {
            if (IsUsingSkill)
                return;
            if (_coolDown > 0)
            {
                return;
            }
            IsUsingSkill = true;
            StartCoroutine(DoDash());
        };
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

    private IEnumerator DoDash()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Floor"));
        if (hit.collider != null)
        {
            _coolDown = CoolDown;
            Vector3 target = hit.point;
            target.y = _basePos.position.y;
            _basePos.LookAt(target);
            _anim.SetSpeed(5);
            _anim.PlaySpecialAnimation();
            while (IsUsingSkill)
            {
                _move.enabled = false;  
                _move.MoveAgent(Vector3.forward * 10);
                Collider[] cols = Physics.OverlapSphere(_basePos.position, 1.5f, LayerMask.GetMask("Enemy"));
                foreach (Collider col in cols)
                {
                    IHittable iHit = col.GetComponent<IHittable>();
                    if (iHit != null)
                        iHit.DamageAgent(1, _basePos.gameObject);
                }
                yield return new WaitForFixedUpdate();
            }
            //_move.SetAgentCollision(false);
            _move.enabled = true;  
            _anim.SetSpeed(1);
        }
    }
}
