using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPartyTurnState : FightBaseState
{
    public override void EnterState(FightStateManager fightStateManager)
    {
        TurnCounter.Instance.IncrementTurnCount();
        //fightStateManager.AlliesTurnStateManager.EntitiesInTeam[0].MeshRenderer.sharedMaterials[1].SetInt("_ShowOutline", 1);
        fightStateManager.AlliesTurnStateManager.SwitchState(fightStateManager.AlliesTurnStateManager.FirstMateState);
    }

    public override void ExitState(FightStateManager fightStateManager)
    {
        //fightStateManager.AlliesTurnStateManager.EntitiesInTeam[fightStateManager.AlliesTurnStateManager.EntitiesInTeam.Count - 1].MeshRenderer.sharedMaterials[1].SetInt("_ShowOutline", 0);
        fightStateManager.AlliesTurnStateManager.SwitchState(null);
        //fightStateManager.AlliesTurnStateManager.CurrentState.ExitState(fightStateManager.AlliesTurnStateManager);
    }

    public override void UpdateState(FightStateManager fightStateManager)
    {

    }
}
