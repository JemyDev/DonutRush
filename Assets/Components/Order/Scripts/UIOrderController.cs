using TMPro;
using UnityEngine;

/// <summary>
/// UI Order controller that updates the order UI based on current order
/// </summary>
public class UIOrderController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private UIOrderLineController _orderLinePrefab;
    [SerializeField] private TMP_Text _totalText;

    private int _totalCalories = 0;
    
    private void Awake()
    {
        GameEventSystem.OnOrderCreated += HandleOrderCreated;
        GameEventSystem.OnIngredientCollected += HandleTotalCaloriesUpdate;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCreated -= HandleOrderCreated;
        GameEventSystem.OnIngredientCollected -= HandleTotalCaloriesUpdate;
    }

    private void HandleOrderCreated(Order order)
    {
        ClearExistingOrderLines();

        foreach (var orderLine in order.OrderLines)
        {
            CreateOrderLineUI(orderLine.Value);
        }
        
        UpdateTotalCalories(order.TotalCalories);
    }
    
    private void ClearExistingOrderLines()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    private void CreateOrderLineUI(OrderLine orderLine)
    {
        var orderLineUI = Instantiate(_orderLinePrefab, transform);
        orderLineUI.AddSprite(orderLine.Ingredient.ingredientSprite);
    }
    
    private void UpdateTotalCalories(int caloriesToAdd)
    {
        _totalCalories += caloriesToAdd;
        _totalText.text = _totalCalories.ToString();
    }
    
    private void HandleTotalCaloriesUpdate(IngredientScriptableObject ingredient)
    {
        UpdateTotalCalories(-ingredient.ingredientScore);
    }
}
