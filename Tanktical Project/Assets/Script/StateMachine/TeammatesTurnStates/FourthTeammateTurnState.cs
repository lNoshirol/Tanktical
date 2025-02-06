using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.FourthCharacterUI.SetActive(false);
        teamStateManager.FourthCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
        teamStateManager.FourthCharacter.sharedMaterials[1].SetInt("_ShowOutline", 1);
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.FourthCharacterUI.SetActive(true);
        teamStateManager.FourthCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
        teamStateManager.FourthCharacter.sharedMaterials[1].SetInt("_ShowOutline", 0);
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
