using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIState
{
    public Action OnStateAction { get; set; }
    public AITransition NextTransition { get; set; }
}
