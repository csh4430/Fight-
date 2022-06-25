using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealState : AIState, ISkillState
{
    public override Action OnStateAction { get; set; }

    [field: SerializeField]
    public override List<AITransition> Transition { get; set; }

    public Transform _basePos = null;
    private Transform _targetPos = null;
    [SerializeField]
    private float _healAmount = 0;
    public Transform TargetPos { get => _targetPos; set { _targetPos = value; } }

    [field: SerializeField]
    public float CoolDown { get; set; }
    private float _coolDown = 0;

    [field: SerializeField]
    public bool IsUsingSkill { get; set; } = false;

    [field: SerializeField]
    public Slider CoolDownSilder { get; set; }

    private AgentAnimation _anim = null;
    private void Awake()
    {
        _anim = _basePos.GetComponent<AgentAnimation>();
        OnStateAction += () =>
        {
            if (IsUsingSkill)
                return;
            if (_coolDown > 0)
            {
                return;
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                _anim.PlaySpecialAnimation();
                _targetPos = hit.transform;
                IHealable iHeal = _targetPos.GetComponent<IHealable>();
                iHeal.HealAgent(_healAmount, _basePos.gameObject);
                _basePos.LookAt(_targetPos);
                _coolDown = CoolDown;
                IsUsingSkill = true;
            }
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
}