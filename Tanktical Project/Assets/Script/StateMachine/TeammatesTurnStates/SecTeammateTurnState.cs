using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecTeammateTurnState : TeamTurnBaseState
{
    public override void EnterState(TeamStateManager teamStateManager)
    {
        Debug.Log("Second teammate turn");
        //teamStateManager.SecondCharacterUI.SetActive(false);
        //teamStateManager.SecondCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
        //teamStateManager.SecondCharacter.sharedMaterials[1].SetInt("_ShowOutline", 1);
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        //teamStateManager.SecondCharacterUI.SetActive(true);
        //teamStateManager.SecondCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
        //teamStateManager.SecondCharacter.sharedMaterials[1].SetInt("_ShowOutline", 0);
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {
    }
}
