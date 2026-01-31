using System;

public class LifeTracker
{
    public Action OnDeath;

    public int CurrentLife { get; private set; }

    public LifeTracker(int life)
    {
        CurrentLife = life;
    }

    public void LoseLife()
    {
        CurrentLife--;
        
        if (CurrentLife <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
