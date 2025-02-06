using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightBaseState

{
    // enter the state
    public abstract void EnterState(FightStateManager fightStateManager);

    // called every frames
    public abstract void UpdateState(FightStateManager fightStateManager);

    // exit the state
    public abstract void ExitState(FightStateManager fightStateManager);
}
