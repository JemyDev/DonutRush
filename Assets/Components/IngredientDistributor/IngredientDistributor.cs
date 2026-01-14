using System.Collections;
using System.Linq;
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
    
    private void HandleIngredientDistribution(Order order)
    {
        _currentOrderIngredients = order.OrderLines.Values.Select(ol => ol.Ingredient).ToArray();
    }

    private void HandleDoorInstantiated()
    {
        // Handle ingredient distribution after a short delay to ensure order is ready
        StartCoroutine(WaitAndDistributeIngredient());
    }
    
    private IEnumerator WaitAndDistributeIngredient()
    {
        yield return new WaitForSeconds(0.1f);
        var randomIndex = Random.Range(0, _currentOrderIngredients.Length);
        GameEventSystem.OnIngredientDistributed?.Invoke(_currentOrderIngredients[randomIndex]);
    }
}
