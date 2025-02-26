using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[1].ActivateDeactivateOutline(1);
        teamStateManager.EntitiesInTeam[1].ShowSkillsPanel();
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[1].ActivateDeactivateOutline(0);
        teamStateManager.EntitiesInTeam[1].HideSkillsPanel();
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
