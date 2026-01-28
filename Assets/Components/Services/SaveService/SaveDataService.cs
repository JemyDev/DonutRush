using UnityEngine;

namespace Services.SaveService
{
    public static class SaveDataService
    {
        private static SaveData _runtimeData;
        private static bool _isDirty;
        
        public static int RunCount => _runtimeData.RunCount;
        public static int HighScore => _runtimeData.HighScore;
        public static int TotalIngredientsCollected => _runtimeData.TotalIngredientsCollected;
        public static int TotalOrdersCompleted => _runtimeData.TotalOrdersCompleted;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            // Reinitialize _isDirty in case of domain reloads
            _isDirty = false;
            if (!SaveService.TryLoad(out _runtimeData))
            {
                _runtimeData = new SaveData();
            }
        }

        public static void UpdateRunCount()
        {
            _runtimeData.RunCount++;
            _isDirty = true;
        }
        
        public static void UpdateHighScore(int score)
        {
            if (score > _runtimeData.HighScore)
            {
                _runtimeData.HighScore = score;
                _isDirty = true;
            }
        }
        
        public static void UpdateTotalIngredientsCollected()
        {
            _runtimeData.TotalIngredientsCollected++;
            _isDirty = true;
        }
        
        public static void UpdateTotalOrdersCompleted()
        {
            _runtimeData.TotalOrdersCompleted++;
            _isDirty = true;
        }
        
        public static void SaveIfDirty()
        {
            if (_isDirty)
            {
                SaveService.Save(_runtimeData);
                _isDirty = false;
            }
        }
    }
}