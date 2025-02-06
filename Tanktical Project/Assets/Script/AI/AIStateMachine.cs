using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public enum States
{

}

public class AIStateMachine : MonoBehaviour
{
    private AIStateBase _currentState;
    private List<AIStateBase> _states = new(){  };

    [field: SerializeField]
    public NavMeshAgent Agent { get; private set; }
    
    [SerializeField, Dropdown("GetStates")] private string _defaultState;
    private List<string> GetStates() => _states.ConvertAll(input => input.GetType().Name);
    
    private void Awake()
    {
        _currentState = _states.Find(input => input.GetType().Name == _defaultState);
        _currentState.OnStateEnter();
    }

    public void TransitionToState(States newState)
    {
        _currentState.OnStateExit();
        _currentState = _states[(int)newState];
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        _currentState.OnStateUpdate(UpdateType.NORMAL);
    }
    
    private void FixedUpdate()
    {
        _currentState.OnStateUpdate(UpdateType.FIXED);
    }
    
    private void LateUpdate()
    {
        _currentState.OnStateUpdate(UpdateType.LATE);
    }
}
