using System;

namespace Services.SaveService
{
    [Serializable]
    public class SaveData
    {
        public int RunCount;
        public int HighScore;
        public int TotalIngredientsCollected;
        public int TotalOrdersCompleted;
    }
}