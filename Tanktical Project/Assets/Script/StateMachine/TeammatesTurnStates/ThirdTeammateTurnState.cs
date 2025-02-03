using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.ThirdCharacter.SetActive(false);
        teamStateManager.ThirdCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.ThirdCharacter.SetActive(true);
        teamStateManager.ThirdCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
