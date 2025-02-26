using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirstTeammateTurnState : TeamTurnBaseState
{
    // Constructor
    public FirstTeammateTurnState()
    {

    }

    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[0].ActivateDeactivateOutline(1);
        teamStateManager.EntitiesInTeam[0].ShowSkillsPanel();
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[0].ActivateDeactivateOutline(0);
        teamStateManager.EntitiesInTeam[0].HideSkillsPanel();
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {

    }
}
