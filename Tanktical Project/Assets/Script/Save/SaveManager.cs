using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CCJsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEngine;

public struct Variable
{
    public Type VariableType;
    public object VariableValue;
}

[Serializable]
public class SavedVariable
{
    public string VariableId;
    public string VariableTypeName;
    public object VariableValue;
}

public static class SaveManager
{
    private static readonly Dictionary<string, Variable> _variables = new();

    public static T GetVariable<T>(string name)
    {
        if (!typeof(T).IsValueType && typeof(T).FullName != typeof(string).FullName) 
            throw new TypeInitializationException(typeof(T).FullName, new("The type must be a value type"));

        if (!_variables.TryGetValue(name, out Variable variable))
            throw new KeyNotFoundException($"Variable {name} isn't saved");

        if (variable.VariableType == typeof(T))
            return (T)variable.VariableValue;
        
        throw new InvalidDataException($"Variable {name} isn't of type {typeof(T).Name}");
    }
    
    public static Tuple<Type, object> GetTypelessVariable(string name)
    {
        if (!_variables.TryGetValue(name, out Variable variable))
            throw new KeyNotFoundException($"Variable {name} isn't saved");

        return new (variable.VariableValue.GetType(), variable.VariableValue);
    }
    
    public static void SetVariable<T>(string name, T value, bool overwrite = true, bool changeType = false)
    {        
        if (!typeof(T).IsValueType && typeof(T).FullName != typeof(string).FullName) 
            throw new TypeAccessException($"The type {typeof(T).FullName} must be a value type");
        
        if (_variables.TryGetValue(name, out Variable variable))
        {
            if (!overwrite) 
                throw new ArgumentException($"Variable {name} is already saved. Set overwrite to true if you want to overwrite it.");
            
            if (variable.VariableType != typeof(T) && !changeType) 
                throw new InvalidDataException($"Variable {name} is already saved as {typeof(T).Name}");

            _variables.Remove(name);
        }
        
        _variables.Add(name, new (){VariableValue = value, VariableType = value.GetType()});
    }
    
    public static void SetVariableNonGeneric(Type T, string name, object value, bool overwrite = true, bool changeType = false)
    {        
        if (!T.IsValueType && T.FullName != typeof(string).FullName) 
            throw new TypeAccessException($"The type {T.FullName} must be a value type");
        
        if (_variables.TryGetValue(name, out Variable variable))
        {
            if (!overwrite) 
                throw new ArgumentException($"Variable {name} is already saved. Set overwrite to true if you want to overwrite it.");
            
            if (variable.VariableType != T && !changeType) 
                throw new InvalidDataException($"Variable {name} is already saved as {T.Name}");

            _variables.Remove(name);
        }
        
        _variables.Add(name, new (){VariableValue = value, VariableType = T});
    }

    public static bool Exists(string name)
    {
        if (!_variables.TryGetValue(name, out Variable variable))
            return false;
        
        return true;
    }
    
    public static bool IsOfType(Type type, string name)
    {
        if (!_variables.TryGetValue(name, out Variable variable))
            throw new KeyNotFoundException($"Variable {name} isn't saved");
        
        if (variable.VariableType != type)
            return false;
        
        return true;
    }

    public static void Save(string filename, bool overwrite = true)
    {
        if (File.Exists(filename) && !overwrite) throw new FileLoadException($"File {filename} already exists, set overwrite to true if you want to overwrite it.");
        
        List<SavedVariable> savedVariables = new();
        foreach (string var in _variables.Keys)
        {
            savedVariables.Add(new()
            {
                VariableId = var,
                VariableTypeName = _variables[var].VariableType.FullName,
                VariableValue = _variables[var].VariableValue,
            });
        }
        
        JsonSerializer serializer = new JsonSerializer();
        serializer.Converters.Add(new Vector2Converter());
		serializer.Converters.Add(new Vector3Converter());
        serializer.NullValueHandling = NullValueHandling.Ignore;

        using (StreamWriter sw = new StreamWriter(@filename))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            serializer.Serialize(writer, savedVariables);
        }
    }

    public static void Load(string filename)
    {
        if (!File.Exists(filename)) throw new FileNotFoundException("Save File named " + filename + " does not exist");

        List<SavedVariable> savedVariables = new();

        using (JsonReader reader = new JsonTextReader(new StreamReader(filename)))
        {
            var deserializer = new JsonSerializer();
            deserializer.Converters.Add(new Vector2Converter());
			deserializer.Converters.Add(new Vector3Converter());
            deserializer.NullValueHandling = NullValueHandling.Ignore;
            
            savedVariables = deserializer.Deserialize<List<SavedVariable>>(reader);
        }

        foreach (SavedVariable var in savedVariables)
        {
            object obj = var.VariableValue;

            Type type = ByName(var.VariableTypeName);

            
            if(type == typeof(Int32))
            {
                obj = (int)((Int64)var.VariableValue);
            }
            else if(type == typeof(float))
            {
                obj = (float)((double)var.VariableValue);
            }
            else if(type == typeof(Vector2))
            {
                obj = ((JObject)var.VariableValue).ToObject<Vector2>();
            }

	    // Add more possible edgecases here (Vector3, for exemple)
            
            SetVariableNonGeneric(type, var.VariableId, obj);
        }
    }
    
    private static Type ByName(string name)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var tt = assembly.GetType(name);
            if (tt != null)
            {
                return tt;
            }
        }

        return null;
    }
}
