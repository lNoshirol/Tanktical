using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[4].ActivateDeactivateOutline(1);
        teamStateManager.EntitiesInTeam[4].ShowSkillsPanel();
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[4].ActivateDeactivateOutline(0);
        teamStateManager.EntitiesInTeam[4].HideSkillsPanel();
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
