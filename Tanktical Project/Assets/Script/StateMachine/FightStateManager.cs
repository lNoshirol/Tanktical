using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStateManager : MonoBehaviour
{
    public List<Entity> allies;
    public List<Entity> enemies;
    public Material AllyOutlineMaterial;
    public Material EnemyOutlineMaterial;

    public TeamStateManager AlliesTurnStateManager;
    public TeamStateManager EnemiesTurnStateManager;

    public FightBaseState CurrentState { get; private set; }
    public AllyPartyTurnState AllyTurnState { get; private set; } = new();
    public EnemyPartyTurnState EnemyTurnState { get; private set; } = new();

    private void Start() 
    {
        AlliesTurnStateManager = new TeamStateManager(allies, this, AllyOutlineMaterial);
        EnemiesTurnStateManager = new TeamStateManager(enemies, this, EnemyOutlineMaterial);
        
        AlliesTurnStateManager.Init();
        EnemiesTurnStateManager.Init();

        CurrentState = EnemyTurnState;
        SwitchState(AllyTurnState);
    }

    public void SwitchState(FightBaseState newState)
    {
        // Exit current state, then enter the new one
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}
