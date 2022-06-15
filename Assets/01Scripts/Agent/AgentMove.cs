using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class AgentMove : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float rotationAngle = 3f;
    private Agent _base;
    private CharacterController _characterController = null;

    private void Awake()
    {
        _base = GetComponent<Agent>();
        _characterController = GetComponent<CharacterController>();
    }

    public void MoveAgent(Vector3 dir)
    {
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
}
