namespace Components.Managers
{
    public class ScoreManager
    {
        public int CurrentScore { get; private set; }

        public void AddScore(int score)
        {
            CurrentScore += score;
        }
    }
}