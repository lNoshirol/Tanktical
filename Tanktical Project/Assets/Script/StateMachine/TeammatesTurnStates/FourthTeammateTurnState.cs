using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.FourthCharacter.SetActive(false);
        teamStateManager.FourthCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.FourthCharacter.SetActive(true);
        teamStateManager.FourthCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
