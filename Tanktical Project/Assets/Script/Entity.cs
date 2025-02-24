using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public TeamStateManager TeamStateManager;
    public MeshRenderer MeshRenderer { get; set; }

    public event Action OnEndTurn;

    // temporary
    public GameObject BlackScreenUI;
    public List<Button> SkillButtons;

    private void Awake()
    {
        MeshRenderer = TryGetComponent(out MeshRenderer mesh) ? mesh : null;
    }

    public void AddTeamStateListeners(int state)
    {
        //foreach (Button button in SkillButtons)
        //{
        //    // Won't show up in the inspector because it is not a persistent listener
        //    button.onClick.AddListener(() => TeamStateManager.SwitchState(TeamStateManager.TeamTurnBaseStates[state]));

        //}
        OnEndTurn += () => TeamStateManager.SwitchState(TeamStateManager.TeamTurnBaseStates[state]);
    }

    public void AddFightStateListeners()
    {
        //foreach (Button button in SkillButtons)
        //{
        //    // Make turn change to enemies if this entity is an ally, else to allies
        //    if (TeamStateManager.FightStateManager.AlliesTurnStateManager == TeamStateManager)
        //    {
        //        button.onClick.AddListener(() => TeamStateManager.FightStateManager.SwitchState(TeamStateManager.FightStateManager.EnemyTurnState));
        //    }
        //    else
        //    {
        //        button.onClick.AddListener(() => TeamStateManager.FightStateManager.SwitchState(TeamStateManager.FightStateManager.AllyTurnState));
        //    }
        //}

        if (TeamStateManager.FightStateManager.AlliesTurnStateManager == TeamStateManager)
        {
            OnEndTurn += () => TeamStateManager.FightStateManager.SwitchState(TeamStateManager.FightStateManager.EnemyTurnState);
        }
        else
        {
            OnEndTurn += () => TeamStateManager.FightStateManager.SwitchState(TeamStateManager.FightStateManager.AllyTurnState);
        }
    }

    public void ActivateDeactivateOutline(int value)
    {
        if (value > 1 | value < 0) return;
        MeshRenderer.sharedMaterials[1].SetInt("_ShowOutline", value);
    }

    public void OnDestroy()
    {
        MeshRenderer.sharedMaterials[1].SetInt("_ShowOutline", 0);
    }

    public void EndTurn()
    {
        OnEndTurn?.Invoke();
    }
}
