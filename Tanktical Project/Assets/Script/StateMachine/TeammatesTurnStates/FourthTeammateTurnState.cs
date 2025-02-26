using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[3].ActivateDeactivateOutline(1);
        teamStateManager.EntitiesInTeam[3].ShowSkillsPanel();
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[3].ActivateDeactivateOutline(0);
        teamStateManager.EntitiesInTeam[3].HideSkillsPanel();
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
