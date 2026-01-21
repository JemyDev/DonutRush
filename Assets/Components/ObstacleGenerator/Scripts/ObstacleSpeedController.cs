using UnityEngine;
using Services.GameEventService;

public class ObstacleSpeedController : MonoBehaviour
{
    [SerializeField] private ObstacleGenerator _obstacleGenerator;
    [SerializeField] private float _speedIncrement = 0.5f;
    [SerializeField] private float _maxSpeed = 10f;

    private float _completedOrders = 0;
    private float _baseSpeed;

    private void Start()
    {
        _baseSpeed = _obstacleGenerator.TranslationSpeed;
        GameEventService.OnOrderCompleted += UpdateObstacleSpeed;
    }
    
    private void OnDestroy()
    {
        GameEventService.OnOrderCompleted -= UpdateObstacleSpeed;
    }

    private void UpdateObstacleSpeed(int score)
    {
        
        _completedOrders++;

        var newSpeed = _baseSpeed + _speedIncrement * Mathf.Sqrt(_completedOrders);
        newSpeed = Mathf.Min(newSpeed, _maxSpeed);

        _obstacleGenerator.IncreaseTranslationSpeed(newSpeed);
    }
}
