using System;

namespace Components.Managers
{
    public class TimerManager
    {
        private readonly float _baseTimer;

        public float RemainingTime { get; private set; }

        public Action OnTimerExpired;

        public TimerManager(float initialTimer)
        {
            _baseTimer = initialTimer;
            RemainingTime = initialTimer;
        }

        public void Update(float deltaTime)
        {
            RemainingTime -= deltaTime;

            if (RemainingTime <= 0)
            {
                OnTimerExpired?.Invoke();
                Reset();
            }
        }

        public void Reset()
        {
            RemainingTime = _baseTimer;
        }
    }
}