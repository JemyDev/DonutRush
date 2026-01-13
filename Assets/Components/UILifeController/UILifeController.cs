using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Life Controller to manage player's life points display
/// </summary>
public class UILifeController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _startLife = 3;
    
    [Header("Life Point Prefab")]
    [SerializeField] private Image _lifePointPrefab;

    private readonly List<Image> _currentLifePoints = new();
    private int CurrentLife => _currentLifePoints.Count;

    private void Start()
    {
        SetBaseLife();
        GameEventSystem.OnPlayerCollision += HandleCollision;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnPlayerCollision -= HandleCollision;
    }

    private void SetBaseLife()
    {
        for (var i = 0; i < _startLife; i++)
        {
            var newLifePoint = Instantiate(_lifePointPrefab, transform);
            _currentLifePoints.Add(newLifePoint);
        }
    }

    private void HandleCollision()
    {
        UpdateLife();
    }

    private void UpdateLife()
    {
        if (CurrentLife == 0) return;
        
        var lastLifePointIndex = CurrentLife - 1;
        var lifePointToRemove = _currentLifePoints[lastLifePointIndex];
        _currentLifePoints.RemoveAt(lastLifePointIndex);
        Destroy(lifePointToRemove.gameObject);
    }
}
