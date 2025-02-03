using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.FirstCharacter.SetActive(false);
        teamStateManager.FirstCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.FirstCharacter.SetActive(true);
        teamStateManager.FirstCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {

    }
}
