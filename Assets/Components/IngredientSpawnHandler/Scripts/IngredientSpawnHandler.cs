using System.Linq;
using UnityEngine;
using Services.GameEventService;

/// <summary>
/// Handles the distribution of ingredients based on the current order
/// </summary>
public class IngredientSpawnHandler : MonoBehaviour
{
    [SerializeField] private IngredientData[] _currentOrderIngredients;
    
    private void Awake()
    {
        GameEventService.OnOrderCreated += HandleOrderCreated;
        GameEventService.OnDoorInstantiated += HandleDoorInstantiated;
        GameEventService.OnIngredientOrderLineCompleted += HandleIngredientOrderLineCompleted;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCreated -= HandleOrderCreated;
        GameEventService.OnDoorInstantiated -= HandleDoorInstantiated;
        GameEventService.OnIngredientOrderLineCompleted -= HandleIngredientOrderLineCompleted;
    }
    
    private void HandleOrderCreated(Order order)
    {
        _currentOrderIngredients = order.OrderLines.Values.Select(ol => ol.Ingredient).ToArray();
    }

    private void HandleIngredientOrderLineCompleted(IngredientData ingredient)
    {
        _currentOrderIngredients = _currentOrderIngredients.Where(i => i != ingredient).ToArray();
    }

    private void HandleDoorInstantiated()
    {
        if (_currentOrderIngredients == null || _currentOrderIngredients.Length == 0)
            return;

        var randomIndex = Random.Range(0, _currentOrderIngredients.Length);
        GameEventService.OnIngredientSpawned?.Invoke(_currentOrderIngredients[randomIndex]);
    }
}
