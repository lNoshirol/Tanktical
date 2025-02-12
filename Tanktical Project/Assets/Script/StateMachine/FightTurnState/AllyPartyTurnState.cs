using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPartyTurnState : FightBaseState
{
    public override void EnterState(FightStateManager fightStateManager)
    {
        Debug.Log("Ally turn start");
    }

    public override void ExitState(FightStateManager fightStateManager)
    {
        Debug.Log("Ally turn end");
    }

    public override void UpdateState(FightStateManager fightStateManager)
    {

    }
}
