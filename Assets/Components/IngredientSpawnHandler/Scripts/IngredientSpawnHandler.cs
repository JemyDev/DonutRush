using System.Linq;
using UnityEngine;
using Services.GameEventService;

/// <summary>
/// Handles the distribution of ingredients based on the current order
/// </summary>
public class IngredientSpawnHandler : MonoBehaviour
{
    [SerializeField] private IngredientData[] _currentOrderIngredients;
    [SerializeField] private IngredientData[] _defaultIngredientsList;
    
    private void Awake()
    {
        GameEventService.OnOrderCreated += HandleIngredientDistribution;
        GameEventService.OnDoorInstantiated += HandleDoorInstantiated;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCreated -= HandleIngredientDistribution;
        GameEventService.OnDoorInstantiated -= HandleDoorInstantiated;
    }
    
    private void HandleIngredientDistribution(Order order)
    {
        _currentOrderIngredients = order.OrderLines.Values.Select(ol => ol.Ingredient).ToArray();
    }

    private void HandleDoorInstantiated()
    {
        // Check if there are ingredients in the current order; if not, use the default list
        if (_currentOrderIngredients == null || _currentOrderIngredients.Length == 0)
        {
            _currentOrderIngredients = _defaultIngredientsList;
        }
        
        var randomIndex = Random.Range(0, _currentOrderIngredients.Length);
        GameEventService.OnIngredientSpawned?.Invoke(_currentOrderIngredients[randomIndex]);
    }
}
