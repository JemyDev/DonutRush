using System;

namespace Services.SaveService
{
    [Serializable]
    public class SaveData
    {
        public int RunCount = 0;
        public int HighScore = 0;
        public int TotalIngredientsCollected = 0;
        public int TotalOrdersCompleted = 0;
        public int LevelIndex = 1;
    }
}