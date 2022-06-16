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
            if(dir.sqrMagnitude > 0.8f)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (dir.z > 0.8f)
                    {
                        _move.OnRunEvent?.Invoke(dir.normalized);
                        return;
                    }
                }
                _move.OnWalkEvent?.Invoke(dir.normalized);
            }
            else if(dir.sqrMagnitude < 1)
            {
                _move.StopMove();
            }
        };

        OnAttackKeyInput += (type) => {
            _attack.OnAttackEvent?.Invoke(type);
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
