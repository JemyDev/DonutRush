using System.Linq;
using UnityEngine;

/// <summary>
/// Handles the distribution of ingredients based on the current order
/// </summary>
public class IngredientDistributor : MonoBehaviour
{
    [SerializeField] private IngredientScriptableObject[] _currentOrderIngredients;
    [SerializeField] private IngredientScriptableObject[] _defaultIngredientsList;
    
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
        GameEventSystem.OnIngredientDistributed?.Invoke(_currentOrderIngredients[randomIndex]);
    }
}
