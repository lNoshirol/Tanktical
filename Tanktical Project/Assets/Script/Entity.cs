using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public TeamStateManager TeamStateManager;
    public MeshRenderer MeshRenderer { get; set; }
    public Characters Character { get; set; }

    public event Action OnEndTurn;

    // temporary
    public GameObject BlackScreenUI;
    public List<Button> SkillButtons;

    private void Awake()
    {
        MeshRenderer = TryGetComponent(out MeshRenderer mesh) ? mesh : null;
        Character = TryGetComponent(out Characters character) ? character : null;
    }

    public void AddTeamStateListeners(int state)
    {
        OnEndTurn += () => TeamStateManager.SwitchState(TeamStateManager.TeamTurnBaseStates[state]);
    }

    public void AddFightStateListeners()
    {
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

    public void ShowSkillsPanel()
    {
        Character.ShowSkillsPanel();
    }

    public void HideSkillsPanel()
    {
        Character.HideSkillsPanel();
    }
}
