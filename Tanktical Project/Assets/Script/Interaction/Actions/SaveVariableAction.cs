using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// An action to store a variable.
/// </summary>
[Serializable]
public class SaveVariableAction : InteractionAction
{
    [Tooltip("The variable name to store")]
    [SerializeField] private string _variableName;

    enum Types
    {
        Int, Float, String, Bool, Vector3, Vector2
    }
    
    [SerializeField]
    private Types _variableType;
    
    [SerializeField] private int _valueInt;
    [SerializeField] private float _valueFloat;
    [SerializeField] private string _valueString;
    [SerializeField] private bool _valueBool;
    [SerializeField] private Vector3 _valueVector3;
    [SerializeField] private Vector3 _valueVector2;
    
    public override Task<int> Execute()
    {
        switch (_variableType)
        {
            case Types.String:
                SaveManager.SetVariable(_variableName, _valueString);
                break;
            case Types.Int:
                SaveManager.SetVariable(_variableName, _valueInt);
                break;
            case Types.Float:
                SaveManager.SetVariable(_variableName, _valueFloat);
                break;
            case Types.Bool:
                SaveManager.SetVariable(_variableName, _valueBool);
                break;
            case Types.Vector2:
                SaveManager.SetVariable(_variableName, _valueVector2);
                break;
            case Types.Vector3:
                SaveManager.SetVariable(_variableName, _valueVector3);
                break;
        }
        
        // Go to the next action.
        return Task.FromResult(0);
    }

}
