using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(AgentAnimation))]
[RequireComponent(typeof(AgentAttack))]
public class AgentMove : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float rotationAngle = 3f;
    private Agent _base;
    private CharacterController _characterController = null;
    private AgentAnimation _anim;
    private AgentAttack _attack;

    public Action<Vector3> OnWalkEvent;
    public Action<Vector3> OnRunEvent;

    private void Awake()
    {
        _base = GetComponent<Agent>();
        _anim = GetComponent<AgentAnimation>();
        _attack = GetComponent<AgentAttack>();
        _characterController = GetComponent<CharacterController>();

        OnWalkEvent += (dir) =>
        {
            if (_attack.IsAttacking)
                return;
            MoveAgent(dir);
            _anim.PlayWalkAnimation();
            _anim.StopRunAnimation();
        };
        OnRunEvent += (dir) =>
        {
            if (_attack.IsAttacking)
                return;
            MoveAgent(dir * 3);
            _anim.PlayRunAnimation();
            _anim.StopWalkAnimation();
        };
    }

    public void MoveAgent(Vector3 dir)
    {
        if(_characterController.enabled == false)
            return;
        int right = 0;
        
        if (dir.z >= 0)
        {
            if (dir.x > 0)
            {
                right = 1;
            }
            else if (dir.x < 0)
            {
                right = -1;
            }
        }
        else if (dir.z < 0)
        {
            if (dir.x > 0)
            {
                right = -1;
            }
            else if (dir.x < 0)
            {
                right = 1;
            }
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + right * rotationAngle, transform.eulerAngles.z), Time.deltaTime * rotationSpeed);

        Vector3 forward = transform.forward * dir.z;
        forward.y += Physics.gravity.y * Time.deltaTime;
        _characterController?.Move(forward * _base.Speed * Time.deltaTime);
    }
    public void StopMove()
    {
        MoveAgent(Vector3.zero);
        _anim.StopRunAnimation();
        _anim.StopWalkAnimation();
    }
}
