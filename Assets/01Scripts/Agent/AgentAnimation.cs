using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    Animator _anime;
    private int WalkHash = Animator.StringToHash("Walk");
    private int RunHash = Animator.StringToHash("Run");
    private int AttackHash = Animator.StringToHash("Attack");
    private int AttackTypeHash= Animator.StringToHash("AttackType");
    private int DieHash= Animator.StringToHash("Die");
    private int DieTypeHash= Animator.StringToHash("DieType");
    private int DamageHash = Animator.StringToHash("Damage");
    private int SpecialHash = Animator.StringToHash("Special");
    private void Awake()
    {
        _anime = GetComponent<Animator>();
    }

    public void PlayDieAnimation(int type)
    {
        _anime.SetTrigger(DieHash);
        _anime.SetInteger(DieTypeHash, type);
    }

    public void PlayWalkAnimation(){
        _anime.SetBool(WalkHash, true);
    }
    
    public void StopWalkAnimation()
    {
        _anime.SetBool(WalkHash, false);
    }

    public void PlayRunAnimation()
    {
        _anime.SetBool(RunHash, true);
    }

    public void StopRunAnimation()
    {
        _anime.SetBool(RunHash, false);
    }

    public void PlayAttackAnimation(int type)
    {
        _anime.SetInteger(AttackTypeHash, type);
        _anime.SetTrigger(AttackHash);
    }

    public void PlayDamageAnimation()
    {
        _anime.SetTrigger(DamageHash);
    }

    public void PlaySpecialAnimation()
    {
        _anime.SetTrigger(SpecialHash);
    }

    public void PauseAnimation()
    {
        _anime.speed = 0;
    }
    
    public void RePlay()
    {
        _anime.speed = 1;
    }
}
