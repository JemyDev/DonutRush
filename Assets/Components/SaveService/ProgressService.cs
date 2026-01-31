using UnityEngine;

namespace Services.SaveService
{
    public static class ProgressService
    {
        private static PlayerProgress _currentProgress;
        private static bool _isDirty;
        
        public static PlayerProgress CurrentProgress => _currentProgress;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            // Reinitialize _isDirty in case of domain reloads
            _isDirty = false;
            if (!ProgressRepository.TryLoad(out _currentProgress))
            {
                _currentProgress = new PlayerProgress();
            }
        }

        public static void RecordRunCount()
        {
            _currentProgress.IncrementRunCount();
            _isDirty = true;
        }
        
        public static void RecordHighScore(int score)
        {
            _currentProgress.SetHighScore(score);
            _isDirty = true;
        }
        
        public static void UpdateTotalIngredientsCollected()
        {
            _currentProgress.IncrementTotalIngredientsCollected();
            _isDirty = true;
        }
        
        public static void UpdateTotalOrdersCompleted()
        {
            _currentProgress.IncrementTotalOrdersCompleted();
            _isDirty = true;
        }
        
        public static void SaveIfDirty()
        {
            if (_isDirty)
            {
                ProgressRepository.Save(_currentProgress);
                _isDirty = false;
            }
        }
    }
}