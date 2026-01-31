using System;
using UnityEngine;

namespace Services.SaveService
{
    [Serializable]
    public class PlayerProgress
    {
        public int RunCount;
        public int HighScore;
        public int TotalIngredientsCollected;
        public int TotalOrdersCompleted;

        public void IncrementRunCount() => RunCount++;
        public void SetHighScore(int score) => HighScore = Mathf.Max(HighScore, score);
        public void IncrementTotalIngredientsCollected() => TotalIngredientsCollected++;
        public void IncrementTotalOrdersCompleted() => TotalOrdersCompleted++;
    }
}