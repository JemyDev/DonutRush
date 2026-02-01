using System.Collections.Generic;
using Services.GameEventService;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controls the rolling obstacles that move towards the player.
/// </summary>
public class ObstacleRollingController : MonoBehaviour
{
    [Header("Rolling Settings")]
    [SerializeField] private float _rollingSpeed;
    [SerializeField] private List<Transform> _spawnPoints;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject[] _rollingObstaclePrefabs;
    
    private Transform _currentSpawnPoint;
    private GameObject _currentRollingObstacle;

    private void Start()
    {
        if (_spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }
        
        _currentSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        AddNewObstacle();
    }

    private void AddNewObstacle()
    {
        var randomIndex = Random.Range(0, _rollingObstaclePrefabs.Length);
        _currentRollingObstacle = Instantiate(_rollingObstaclePrefabs[randomIndex], _currentSpawnPoint.transform);
    }

    private void Update()
    {
        if (!_currentRollingObstacle) return;

        _currentRollingObstacle.transform.Translate(Vector3.back * (_rollingSpeed * Time.deltaTime));

        if (_currentRollingObstacle.transform.position.z < -10f)
        {
            Destroy(_currentRollingObstacle);
        }
    }
}
