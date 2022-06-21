using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : AIState
{
    public override Action OnStateAction { get; set; }
    
    [field: SerializeField]
    public override List<AITransition> Transition { get; set; }

    private AgentAnimation _anime = null;
    public Transform _basePos = null;
    private Transform _targetPos = null;
    [SerializeField]
    private float _healAmount = 0;
    public Transform TargetPos { get => _targetPos; set { _targetPos = value; } }
    private AgentAnimation _anim = null;
    private void Awake()
    {
        _anim = _basePos.GetComponent<AgentAnimation>();
        OnStateAction += () =>
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                _anim.PlaySpecialAnimation();
                _targetPos = hit.transform;
                IHealable iHeal = _targetPos.GetComponent<IHealable>();
                iHeal.HealAgent(_healAmount, _basePos.gameObject);
            }
        };
    }
}
