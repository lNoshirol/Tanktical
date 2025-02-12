using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamStateManager
{
    protected Material _outlineMaterial;
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

    private List<Entity> _entitiesInTeam = new();

    public TeamTurnBaseState CurrentState { get; private set; }

    // temporaire
    private int state = 0;

    // Constructor
    public TeamStateManager(List<Entity> entitiesInTeam, FightStateManager fightStateManager, Material outline)
    {
        _entitiesInTeam = entitiesInTeam;
        FightStateManager = fightStateManager;
        _outlineMaterial = outline;

        FirstMateState = (FirstTeammateTurnState)TeamTurnBaseStates[0];
        SecondMateState = (SecTeammateTurnState)TeamTurnBaseStates[1];
        ThirdMateState = (ThirdTeammateTurnState)TeamTurnBaseStates[2];
        FourthMateState = (FourthTeammateTurnState)TeamTurnBaseStates[3];
        FifthMateState = (FifthTeammateTurnState)TeamTurnBaseStates[4];
    }

    public void Init()
    {
        // iterate over team entities
        for (int i = 0; i < _entitiesInTeam.Count; i++)
        {
            state = i + 1;
            _entitiesInTeam[i].TeamStateManager = this;

            if (i == _entitiesInTeam.Count - 1)
            {
                _entitiesInTeam[_entitiesInTeam.Count - 1].AddFightStateListeners();
            }
            else
            {
                _entitiesInTeam[i].AddTeamStateListeners(state);
            }
        }

        CurrentState = FirstMateState;
        SwitchState(FirstMateState);
    }

    public void SwitchState(TeamTurnBaseState newState)
    {
        // Exit current state, then enter the new one
        CurrentState.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    private void SwitchState()
    {
        Debug.Log("GNNNNN");
        SwitchState(TeamTurnBaseStates[state]);
    }
}
