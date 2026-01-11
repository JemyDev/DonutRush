using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle current order and updates it
/// </summary>
public class OrderController : MonoBehaviour
{
    private Dictionary<string, OrderLine> _currentOrder;
    [SerializeField] private int _remainingQuantity;
    
    private bool IsOrderCompleted => _remainingQuantity == 0;
    
    private void Start()
    {
        GameEventSystem.OnOrderCreated += HandleOrderCreation;
        GameEventSystem.OnIngredientCollected += HandleOrderUpdate;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCreated -= HandleOrderCreation;
        GameEventSystem.OnIngredientCollected -= HandleOrderUpdate;
    }

    private void HandleOrderCreation(Dictionary<string, OrderLine> orderLines)
    {
        var newOrder = new Dictionary<string, OrderLine>(orderLines);
        SetCurrentOrder(newOrder);
    }
    
    private void HandleOrderUpdate(string ingredientName)
    {
        UpdateOrderLine(ingredientName);
    }

    private void SetCurrentOrder(Dictionary<string, OrderLine> currentOrder)
    {
        _currentOrder = currentOrder;
        _remainingQuantity = 0;
        
        foreach (var orderLine in _currentOrder)
        {
            _remainingQuantity += orderLine.Value.Quantity;
        }
    }

    private void UpdateOrderLine(string ingredientName)
    {
        if (_currentOrder == null) return;
        
        if (_currentOrder.TryGetValue(ingredientName, out var orderLine) && orderLine.Quantity > 0)
        {
            orderLine.Quantity--;
            _currentOrder[ingredientName] = orderLine;
            _remainingQuantity--;
        }
        
        foreach (var ol in _currentOrder)
        {
            Debug.Log("Ingredient: " + ol.Key + ", Quantity: " + ol.Value.Quantity);
        }

        if (IsOrderCompleted && _currentOrder.Count > 0)
        {
            _currentOrder.Clear();
            GameEventSystem.OnOrderCompleted?.Invoke();
        }
    }
}
