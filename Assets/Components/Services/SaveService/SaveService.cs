using System;
using System.IO;
using UnityEngine;

namespace Services.SaveService
{
    public static class SaveService
    {
        private const string FILE_NAME = "game_save_data.json";
        private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_NAME);

        public static bool Save(SaveData saveData)
        {
            try
            {
                var json = JsonUtility.ToJson(saveData);
                File.WriteAllText(FilePath, json);
                Debug.Log("Game saved in " + FilePath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save game data: {e.Message}");
                return false;
            }
        }

        public static bool TryLoad(out SaveData saveData)
        {
            string json;

            try
            {
                json = File.ReadAllText(FilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Unable to read save file: " + e.Message);
                saveData = null;

                return false;
            }

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError("No save data found.");
                saveData = null;

                return false;
            }

            var result = JsonUtility.FromJson<SaveData>(json);
            saveData = result;

            return true;
        }
    }
}
