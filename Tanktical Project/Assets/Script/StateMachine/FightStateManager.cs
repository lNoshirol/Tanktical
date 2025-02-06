using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStateManager : MonoBehaviour
{
    private TeamStateManager _alliesTurnStateManager = new();
    private TeamStateManager _enemiesTurnStateManager = new();

    public FightBaseState CurrentState { get; private set; }

    public void SwitchState(FightBaseState newState)
    {
        // Exit current state, then enter the new one
        CurrentState.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}
