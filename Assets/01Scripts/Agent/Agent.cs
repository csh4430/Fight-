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
    public float OriginalHp;
    public float Attack;
    public float Defence;
    public float Speed;

    protected Action OnDied;
    protected Action<float, GameObject> OnDamaged;

    private AgentAnimation _anime;
    private AgentHpBar _hpBar;
    protected CharacterController _controller;
    private DamageParticle _damageParticle;
    private AIBase _ai;

    protected virtual void Awake()
    {
        OriginalHp = Hp;
        _controller = GetComponent<CharacterController>();
        _anime = GetComponent<AgentAnimation>();
        _ai = transform.Find("AI")?.GetComponent<AIBase>();
        _hpBar = GetComponent<AgentHpBar>();
        _damageParticle = GetComponent<DamageParticle>();
        OnDied += () =>
        {
            _anime.PlayDieAnimation(Random.Range(1, 3));
            if(_ai != null)
                _ai.enabled = false;
        };

        OnDamaged += (damage, attacker) =>
        {
            _anime.PlayDamageAnimation();
            _damageParticle.RenderParticle(transform);
            Debug.Log($"{attacker.name} attacked with {damage} Damages.");
            _hpBar?.SetHpBar(Hp, OriginalHp);
        };
    }
}
