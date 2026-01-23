using System.Collections.Generic;
using UnityEngine;

namespace Components.ScriptableObjectDatabase
{
    public static class ScriptableObjectDatabase<T> where T : ScriptableObject
    {
        private static readonly Dictionary<string, T> DATABASE = new();
        
        private static void Initialize()
        {
            DATABASE.Clear();
            var scriptableObjects = Resources.LoadAll<T>("Data/" + typeof(T).Name);

            foreach (var scriptableObject in scriptableObjects)
            {
                DATABASE.Add(scriptableObject.name, scriptableObject);
            }
        }

        public static T GetByName(string name)
        {
            if (DATABASE.TryGetValue(name, out var result))
            {
                return result;
            }
            
            Debug.LogWarning($"Scriptable object {name} not found");
            return null;
        }
    }
}