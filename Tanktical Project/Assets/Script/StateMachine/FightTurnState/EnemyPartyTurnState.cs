using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartyTurnState : FightBaseState
{
    public override void EnterState(FightStateManager fightStateManager)
    {
        Debug.Log("Enemy turn start");
        fightStateManager.EnemiesTurnStateManager.SwitchState(fightStateManager.EnemiesTurnStateManager.FirstMateState);
    }

    public override void ExitState(FightStateManager fightStateManager)
    {
        Debug.Log("Enemy turn end");
        fightStateManager.EnemiesTurnStateManager.SwitchState(null);
        //fightStateManager.EnemiesTurnStateManager.CurrentState.ExitState(fightStateManager.EnemiesTurnStateManager);
    }

    public override void UpdateState(FightStateManager fightStateManager)
    {
        throw new System.NotImplementedException();
    }
}
