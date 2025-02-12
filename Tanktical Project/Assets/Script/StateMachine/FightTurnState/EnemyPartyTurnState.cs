using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartyTurnState : FightBaseState
{
    public override void EnterState(FightStateManager fightStateManager)
    {
        Debug.Log("Enemy turn start");
    }

    public override void ExitState(FightStateManager fightStateManager)
    {
        Debug.Log("Enemy turn end");
    }

    public override void UpdateState(FightStateManager fightStateManager)
    {
        throw new System.NotImplementedException();
    }
}
