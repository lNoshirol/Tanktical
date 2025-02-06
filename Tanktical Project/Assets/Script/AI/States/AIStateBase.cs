using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateType
{
    NORMAL,
    FIXED,
    LATE
}

[Serializable]
public abstract class AIStateBase
{
    public abstract void Init(AIStateMachine machine, params object[] data);
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate(UpdateType type);
    public abstract void OnStateExit();
}
