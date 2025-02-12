using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public TeamStateManager TeamStateManager;
    public MeshRenderer MeshRenderer;
    public GameObject BlackScreenUI;
    public List<Button> SkillButtons; 

    public void AddTeamStateListeners(int state)
    {
        foreach (Button button in SkillButtons)
        { 
            // Won't show up in the inspector because it is not a persistent listener
            button.onClick.AddListener(() => TeamStateManager.SwitchState(TeamStateManager.TeamTurnBaseStates[state]));

        }
    }

    public void AddFightStateListeners()
    {
        foreach (Button button in SkillButtons)
        {
            if (TeamStateManager != TeamStateManager.FightStateManager.AlliesTurnStateManager && TeamStateManager != TeamStateManager.FightStateManager.EnemiesTurnStateManager)
            {
                print("c'est la mer noire");
                return;
            }
            // Make turn change to enemies if this entity is an ally, else to allies
            if (TeamStateManager.FightStateManager.AlliesTurnStateManager == TeamStateManager)
            {
                print("allié");
                button.onClick.AddListener(() => TeamStateManager.FightStateManager.SwitchState(TeamStateManager.FightStateManager.EnemyTurnState));
            }
            else
            {
                print("ennemi");
                button.onClick.AddListener(() => TeamStateManager.FightStateManager.SwitchState(TeamStateManager.FightStateManager.AllyTurnState));
            }
        }
    }
}
