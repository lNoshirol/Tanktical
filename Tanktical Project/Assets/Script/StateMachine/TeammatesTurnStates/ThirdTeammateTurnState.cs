using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[2].ActivateDeactivateOutline(1);
        teamStateManager.EntitiesInTeam[2].ShowSkillsPanel();
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[2].ActivateDeactivateOutline(0);
        teamStateManager.EntitiesInTeam[2].HideSkillsPanel();
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
