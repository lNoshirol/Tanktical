using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        Debug.Log("Fifth teammate turn");
        teamStateManager.EntitiesInTeam[4].ActivateDeactivateOutline(1);
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[4].ActivateDeactivateOutline(0);
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
