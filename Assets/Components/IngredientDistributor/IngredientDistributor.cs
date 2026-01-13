using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the distribution of ingredients based on the current order
/// </summary>
public class IngredientDistributor : MonoBehaviour
{
    [SerializeField] private IngredientScriptableObject[] _currentOrderIngredients;
    
    private void Awake()
    {
        GameEventSystem.OnOrderCreated += HandleIngredientDistribution;
        GameEventSystem.OnDoorInstantiated += HandleDoorInstantiated;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCreated -= HandleIngredientDistribution;
        GameEventSystem.OnDoorInstantiated -= HandleDoorInstantiated;
    }
    
    private void HandleIngredientDistribution(Dictionary<string, OrderLine> order)
    {
        _currentOrderIngredients = order.Values.Select(ol => ol.Ingredient).ToArray();
    }

    private void HandleDoorInstantiated()
    {
        var randomIndex = Random.Range(0, _currentOrderIngredients.Length);
        GameEventSystem.OnIngredientDistributed?.Invoke(_currentOrderIngredients[randomIndex]);
    }
}
