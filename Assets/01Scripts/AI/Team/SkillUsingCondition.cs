using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsingCondition : AICondition
{
    public GameObject SkillObject;
    private ISkillState _skillState;
    public override bool CheckCondition()
    {
        return _skillState.IsUsingSkill;
    }
    private void Awake()
    {
        _skillState = SkillObject.GetComponent<ISkillState>();
    }
}
