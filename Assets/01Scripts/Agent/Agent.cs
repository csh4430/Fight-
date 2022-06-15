using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AgentAnimation))]
[RequireComponent(typeof(CharacterController))]
public class Agent : MonoBehaviour
{
    public float Hp;
    public float Attack;
    public float Defence;
    public float Speed;

    protected Action OnDied;
    protected Action<float, GameObject> OnDamaged;

    private AgentAnimation _anime;
    protected CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anime = GetComponent<AgentAnimation>();
        OnDied += () =>
        {
            _anime.PlayDieAnimation(Random.Range(1, 3));
        };

        OnDamaged += (damage, attacker) =>
        {
            _anime.PlayDamageAnimation();
        };
    }
}
