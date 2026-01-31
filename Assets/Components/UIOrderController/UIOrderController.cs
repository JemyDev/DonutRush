using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Services.GameEventService;

/// <summary>
/// UI Order controller that updates the order UI based on current order
/// </summary>
public class UIOrderController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private UIOrderLineController _orderLinePrefab;
    [SerializeField] private TMP_Text _totalText;

    private Order _currentOrder;
    private readonly Dictionary<string, UIOrderLineController> _orderLineControllers = new();
    private int TotalOrderCalories => _currentOrder.TotalCalories;
    
    private void Awake()
    {
        GameEventService.OnOrderCreated += HandleOrderCreated;
        GameEventService.OnIngredientCollected += HandleTotalCaloriesUpdate;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCreated -= HandleOrderCreated;
        GameEventService.OnIngredientCollected -= HandleTotalCaloriesUpdate;
    }

    private void HandleOrderCreated(Order order)
    {
        ClearExistingOrderLines();
        _currentOrder = order;

        foreach (var orderLine in order.OrderLines)
        {
            CreateOrderLineUI(orderLine.Value);
        }
        
        UpdateTotalCalories();
    }
    
    private void ClearExistingOrderLines()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _orderLineControllers.Clear();
    }
    
    private void CreateOrderLineUI(OrderLine orderLine)
    {
        var orderLineUI = Instantiate(_orderLinePrefab, transform);
        orderLineUI.SetSprite(orderLine.Ingredient.Sprite);
        orderLineUI.UpdateQuantity(orderLine.Quantity);
        
        _orderLineControllers[orderLine.Ingredient.Name] = orderLineUI;
    }
    
    private void UpdateTotalCalories()
    {
        _totalText.text = TotalOrderCalories.ToString();
    }
    
    private void HandleTotalCaloriesUpdate(IngredientData ingredient)
    {
        if (_orderLineControllers.TryGetValue(ingredient.Name, out var orderLineController))
        {
            var orderLine = _currentOrder.OrderLines[ingredient.Name];
            orderLineController.UpdateQuantity(orderLine.Quantity);
        }
        
        UpdateTotalCalories();
    }
}
