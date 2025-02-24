using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEndState : FightBaseState
{
    public override void EnterState(FightStateManager fightStateManager)
    {
        Debug.Log("Fight End Start");
    }

    public override void ExitState(FightStateManager fightStateManager)
    {
        Debug.Log("Fight End End");
    }

    public override void UpdateState(FightStateManager fightStateManager)
    {
        throw new System.NotImplementedException();
    }
}
