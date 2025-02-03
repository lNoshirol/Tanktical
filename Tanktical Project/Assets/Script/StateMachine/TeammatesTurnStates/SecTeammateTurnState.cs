using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.SecondCharacter.SetActive(false);
        teamStateManager.SecondCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
        Debug.Log(teamStateManager.SecondCharacter.gameObject);
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.SecondCharacter.SetActive(true);
        teamStateManager.SecondCharacter.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
