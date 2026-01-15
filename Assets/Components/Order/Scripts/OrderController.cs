using UnityEngine;

/// <summary>
/// Handle current order and updates it
/// </summary>
public class OrderController : MonoBehaviour
{
    private Order _currentOrder;
    private int _remainingQuantity;
    private bool IsOrderCompleted => _remainingQuantity == 0;
    
    private void Awake()
    {
        GameEventSystem.OnOrderCreated += HandleOrderCreation;
        GameEventSystem.OnIngredientCollected += HandleOrderUpdate;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCreated -= HandleOrderCreation;
        GameEventSystem.OnIngredientCollected -= HandleOrderUpdate;
    }

    private void HandleOrderCreation(Order order)
    {
        SetCurrentOrder(order);
    }
    
    private void HandleOrderUpdate(IngredientScriptableObject ingredient)
    {
        UpdateOrderLine(ingredient.ingredientName);
    }

    private void SetCurrentOrder(Order currentOrder)
    {
        _currentOrder = currentOrder;
        _remainingQuantity = 0;
        
        foreach (var orderLine in _currentOrder.OrderLines)
        {
            _remainingQuantity += orderLine.Value.Quantity;
        }
    }

    private void UpdateOrderLine(string ingredientName)
    {
        if (_currentOrder.OrderLines == null) return;
        
        if (_currentOrder.OrderLines.TryGetValue(ingredientName, out var orderLine) && orderLine.Quantity > 0)
        {
            orderLine.Quantity--;
            _currentOrder.OrderLines[ingredientName] = orderLine;
            _remainingQuantity--;
        }
        
        if (IsOrderCompleted && _currentOrder.OrderLines.Count > 0)
        {
            _currentOrder.OrderLines.Clear();
            GameEventSystem.OnOrderCompleted?.Invoke();
        }
    }
}
