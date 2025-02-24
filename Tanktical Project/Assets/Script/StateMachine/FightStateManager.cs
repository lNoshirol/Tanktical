using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStateManager
{
    public List<Entity> Allies;
    public List<Entity> Enemies;

    public TeamStateManager AlliesTurnStateManager;
    public TeamStateManager EnemiesTurnStateManager;

    public FightBaseState CurrentState { get; private set; }
    public AllyPartyTurnState AllyTurnState { get; private set; } = new();
    public EnemyPartyTurnState EnemyTurnState { get; private set; } = new();

    public FightStateManager(List<Entity> allies, List<Entity> enemies) { 
        Allies = allies;
        Enemies = enemies;

        Init();
    }


    private void Init() 
    {
        AlliesTurnStateManager = new TeamStateManager(Allies, this);
        EnemiesTurnStateManager = new TeamStateManager(Enemies, this);
        
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
