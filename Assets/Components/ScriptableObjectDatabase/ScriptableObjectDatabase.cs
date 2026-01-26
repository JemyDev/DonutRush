using System;
using System.Collections.Generic;
using Components.Data;
using UnityEngine;

namespace Components.SODatabase
{
    public static class ScriptableObjectDatabase
    {
        private static readonly Dictionary<Type, Dictionary<string, ScriptableObject>> DATABASE = new();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            DATABASE.Clear();
            
            Register<LevelParametersData>();
            Register<IngredientData>();
        }

        private static void Register<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (DATABASE.ContainsKey(type))
            {
                Debug.Log("Type already registered: " + type.Name);
                return;
            }

            DATABASE[type] = new Dictionary<string, ScriptableObject>();
            
            var scriptableObjects = Resources.LoadAll<T>("");
            foreach (var scriptableObject in scriptableObjects)
            {
                DATABASE[type][scriptableObject.name] = scriptableObject;
            }
            
            Debug.Log($"Registered {scriptableObjects.Length} scriptable objects of type {type.Name}");
        }

        public static T Get<T>(string name) where T : ScriptableObject
        {
            var type = typeof(T);

            if (DATABASE.TryGetValue(type, out var dataTypes))
            {
                if (dataTypes.TryGetValue(name, out var scriptableObject))
                {
                    return scriptableObject as T;
                }
            }
            
            Debug.LogError($"ScriptableObject of type {type.Name} with name {name} not found.");
            return null;
        }
        
        public static T[] GetAll<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (DATABASE.TryGetValue(type, out var dataTypes))
            {
                var list = new List<T>();
                foreach (var scriptableObject in dataTypes.Values)
                {
                    if (scriptableObject is T typedObject)
                    {
                        list.Add(typedObject);
                    }
                }
                return list.ToArray();
            }
            
            Debug.LogError($"No ScriptableObjects of type {type.Name} found.");
            return Array.Empty<T>();
        }
    }
}