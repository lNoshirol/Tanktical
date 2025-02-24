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
        Debug.Log("First teammate turn");
        teamStateManager.EntitiesInTeam[0].ActivateDeactivateOutline(1);
        //teamStateManager.FirstCharacterUI.SetActive(false);
        //teamStateManager.FirstCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.6f;
        //teamStateManager.FirstCharacter.sharedMaterials[1].SetInt("_ShowOutline", 1);
    }

    public override void ExitState(TeamStateManager teamStateManager)
    {
        teamStateManager.EntitiesInTeam[0].ActivateDeactivateOutline(0);
        //teamStateManager.FirstCharacterUI.SetActive(true);
        //teamStateManager.FirstCharacterUI.gameObject.transform.parent.localScale = Vector3.one * 0.4f;
        //teamStateManager.FirstCharacter.sharedMaterials[1].SetInt("_ShowOutline", 0);
    }

    public override void UpdateState(TeamStateManager teamStateManager)
    {

    }
}
