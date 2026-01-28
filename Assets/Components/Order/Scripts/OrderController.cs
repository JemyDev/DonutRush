using UnityEngine;
using Services.GameEventService;
using Services.SaveService;

/// <summary>
/// Handle current order and updates it
/// </summary>
public class OrderController : MonoBehaviour
{
    private Order _currentOrder;
    private bool IsOrderCompleted => _currentOrder.TotalCalories == 0;
    private int _scoreToAdd;
    
    private void Awake()
    {
        GameEventService.OnOrderCreated += HandleOrderCreation;
        GameEventService.OnIngredientCollected += HandleOrderUpdate;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCreated -= HandleOrderCreation;
        GameEventService.OnIngredientCollected -= HandleOrderUpdate;
    }

    private void HandleOrderCreation(Order order)
    {
        SetCurrentOrder(order);
    }
    
    private void HandleOrderUpdate(IngredientData ingredient)
    {
        UpdateOrderLine(ingredient.Name);
    }

    private void SetCurrentOrder(Order currentOrder)
    {
        _currentOrder = currentOrder;
        _scoreToAdd = _currentOrder.TotalCalories;
    }

    private void UpdateOrderLine(string ingredientName)
    {
        if (_currentOrder.OrderLines == null) return;
        
        if (_currentOrder.OrderLines.TryGetValue(ingredientName, out var orderLine) && orderLine.Quantity > 0)
        {
            orderLine.DecreaseQuantity();
            _currentOrder.OrderLines[ingredientName] = orderLine;
            
            // Save total ingredients collected
            SaveDataService.UpdateTotalIngredientsCollected();
        }

        if (IsOrderCompleted && _currentOrder.OrderLines.Count > 0)
        {
            ValidateOrder();
        }
    }

    private void ValidateOrder()
    {
        _currentOrder.OrderLines.Clear();
        SaveDataService.UpdateTotalOrdersCompleted();
        GameEventService.OnOrderCompleted?.Invoke(_scoreToAdd);
    }
}
