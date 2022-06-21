using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickCondition : AICondition
{
    public KeyCode inputKey;
    public override bool CheckCondition()
    {
        return Input.GetKeyDown(inputKey);
    }
}
