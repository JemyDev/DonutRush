using UnityEngine;

public class ObstacleSpeedController : MonoBehaviour
{
    [SerializeField] private ObstacleGenerator _obstacleGenerator;
    [SerializeField] private float _speedIncrement = 0.5f;
    [SerializeField] private float _maxSpeed = 10f;

    private float _completedOrders = 0;
    private float _baseSpeed;

    void Start()
    {
        _baseSpeed = _obstacleGenerator.TranslationSpeed;
        GameEventSystem.OnOrderCompleted += UpdateObstacleSpeed;
    }
    
    private void OnDestroy()
    {
        GameEventSystem.OnOrderCompleted -= UpdateObstacleSpeed;
    }

    private void UpdateObstacleSpeed(int score)
    {
        
        ++_completedOrders;

        var newSpeed = _baseSpeed + _speedIncrement * Mathf.Sqrt(_completedOrders);
        newSpeed = Mathf.Min(newSpeed, _maxSpeed);

        _obstacleGenerator.IncreaseTranslationSpeed(newSpeed);
    }
}
