using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        teamStateManager.ThirdCharacterUI.SetActive(false);
        teamStateManager.ThirdCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
        teamStateManager.ThirdCharacter.sharedMaterials[1].SetInt("_ShowOutline", 1);
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.ThirdCharacterUI.SetActive(true);
        teamStateManager.ThirdCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
        teamStateManager.ThirdCharacter.sharedMaterials[1].SetInt("_ShowOutline", 0);
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
