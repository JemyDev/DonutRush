using System;

public class OrderTimerTracker
{
    private readonly float _baseTimer;

    public float RemainingTime { get; private set; }

    public Action OnTimerExpired;

    public OrderTimerTracker(float initialTimer)
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