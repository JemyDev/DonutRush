using System;

namespace Components.Managers
{
    public class LifeManager
    {
        public Action OnDeath;

        public int CurrentLife { get; private set; }

        public LifeManager(int life)
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
}