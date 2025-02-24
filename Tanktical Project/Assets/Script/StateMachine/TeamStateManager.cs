using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamStateManager
{
    public FightStateManager FightStateManager;

    public FirstTeammateTurnState FirstMateState;
    public SecTeammateTurnState SecondMateState;
    public ThirdTeammateTurnState ThirdMateState;
    public FourthTeammateTurnState FourthMateState;
    public FifthTeammateTurnState FifthMateState;

    public List<TeamTurnBaseState> TeamTurnBaseStates = new()
    {
        new FirstTeammateTurnState(),
        new SecTeammateTurnState(),
        new ThirdTeammateTurnState(),
        new FourthTeammateTurnState(),
        new FifthTeammateTurnState()
    };

    public List<Entity> EntitiesInTeam = new();

    public TeamTurnBaseState CurrentState { get; private set; }

    // temporaire
    private int state = 0;

    // Constructor
    public TeamStateManager(List<Entity> entitiesInTeam, FightStateManager fightStateManager)
    {
        EntitiesInTeam = entitiesInTeam;
        FightStateManager = fightStateManager;

        FirstMateState = (FirstTeammateTurnState)TeamTurnBaseStates[0];
        SecondMateState = (SecTeammateTurnState)TeamTurnBaseStates[1];
        ThirdMateState = (ThirdTeammateTurnState)TeamTurnBaseStates[2];
        FourthMateState = (FourthTeammateTurnState)TeamTurnBaseStates[3];
        FifthMateState = (FifthTeammateTurnState)TeamTurnBaseStates[4];
    }

    public void Init()
    {
        // iterate over team entities
        for (int i = 0; i < EntitiesInTeam.Count; i++)
        {
            state = i + 1;
            EntitiesInTeam[i].TeamStateManager = this;
            EntitiesInTeam[i].MeshRenderer.sharedMaterials[1].SetInt("_ShowOutline", 0);

            if (i == EntitiesInTeam.Count - 1)
            {
                EntitiesInTeam[EntitiesInTeam.Count - 1].AddFightStateListeners();
            }
            else
            {
                EntitiesInTeam[i].AddTeamStateListeners(state);
            }
        }

        //CurrentState = FourthMateState;
        //SwitchState(FirstMateState);
        //CurrentState.ExitState(this);
    }

    public void SwitchState(TeamTurnBaseState newState)
    {
        // Exit current state (if there is one), then enter the new one
        CurrentState?.ExitState(this);
        CurrentState = newState;
        CurrentState?.EnterState(this);
    }
}
