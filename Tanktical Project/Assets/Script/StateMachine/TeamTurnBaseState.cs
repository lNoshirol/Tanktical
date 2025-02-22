using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TeamTurnBaseState
{
    public Entity CorrespondingEntity { get; set; }

    // Constructor
    public TeamTurnBaseState() { }

    // enter the state
    public abstract void EnterState(TeamStateManager teamStateManager);

    // called every frames
    public abstract void UpdateState(TeamStateManager teamStateManager);

    // exit the state
    public abstract void ExitState(TeamStateManager teamStateManager);
}
