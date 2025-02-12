using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        Debug.Log("Fifth teammate turn");
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
