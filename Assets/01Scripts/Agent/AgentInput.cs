using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AgentMove))]
[RequireComponent(typeof(AgentAnimation))]
[RequireComponent(typeof(AgentAttack))]
public class AgentInput : MonoBehaviour
{
    private Action<Vector3> OnWalkKeyInput;
    private Action<int> OnAttackKeyInput;
    private AgentMove _move;
    private AgentAnimation _anim;
    private AgentAttack _attack;

    private void Awake()
    {
        _move = GetComponent<AgentMove>();
        _anim = GetComponent<AgentAnimation>();
        _attack = GetComponent<AgentAttack>();
        OnWalkKeyInput += (dir) =>
        {
            if (_attack.IsAttacking)
                return;
            if(dir.sqrMagnitude > 0)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if(dir.z > 0)
                    {
                        _move.MoveAgent(dir.normalized * 3);
                        _anim.PlayRunAnimation();
                        _anim.StopWalkAnimation();
                        return;
                    }
                }
                _move.MoveAgent(dir.normalized);
                _anim.PlayWalkAnimation();
                _anim.StopRunAnimation();
            }
            else if(dir.sqrMagnitude < 1)
            {
                _anim.StopRunAnimation();
                _anim.StopWalkAnimation();
            }
        };

        OnAttackKeyInput += (type) => {
            if (_attack.IsAttacking)
                return;
            _attack.IsAttacking = true;
            _anim.PlayAttackAnimation(type);
        };
    }

    private void Update()
    {
        OnWalkKeyInput?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnAttackKeyInput?.Invoke(Random.Range(1, 3));
        }
    }
}
