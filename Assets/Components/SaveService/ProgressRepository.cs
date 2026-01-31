using System;
using System.IO;
using UnityEngine;

namespace Services.SaveService
{
    public static class ProgressRepository
    {
        private const string FILE_NAME = "game_save_data.json";
        private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_NAME);

        public static void Save(PlayerProgress playerProgress)
        {
            var json = JsonUtility.ToJson(playerProgress);
            File.WriteAllText(FilePath, json);
            Debug.Log("Game saved in " + FilePath);
        }

        public static bool TryLoad(out PlayerProgress playerProgress)
        {
            string json;

            try
            {
                json = File.ReadAllText(FilePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Unable to read save file: " + e.Message);
                playerProgress = null;

                return false;
            }

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError("No save data found.");
                playerProgress = null;

                return false;
            }

            var result = JsonUtility.FromJson<PlayerProgress>(json);
            playerProgress = result;

            return true;
        }
    }
}
