using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ISkillState
{
    public float CoolDown { get; set; }
    public bool IsUsingSkill { get; set; }
    public Slider CoolDownSilder { get; set; }
    public AudioClip skillClip { get; set; }
}
