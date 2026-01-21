using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Services.GameEventService;

/// <summary>
/// Handle order generation with a random pick of Scriptable Object ingredients
/// </summary>
public class OrderGenerator : MonoBehaviour
{
    [Header("Scriptable Objects Parameters")]
    [SerializeField] private IngredientData[] _availableIngredients;
    
    [Header("Order Parameters")]
    [SerializeField] private int _maxIngredientsPerOrder = 3;
    [SerializeField] private int _minIngredientsPerOrderLine = 1;
    [SerializeField] private int _maxIngredientsPerOrderLine = 5;

    private void Awake()
    {
        GameEventService.OnOrderCompleted += HandleCompletedOrder;
    }

    private void Start()
    {
        // Create a new order on start
        CreateNewOrder();
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCompleted -= HandleCompletedOrder;
    }
    
    private void CreateNewOrder()
    {
        var newOrder = GenerateOrder();
        GameEventService.OnOrderCreated?.Invoke(newOrder);
    }

    private Order GenerateOrder()
    {
        var newOrderLines = new Dictionary<string, OrderLine>();
        
        for (var i = 0; i < _maxIngredientsPerOrder; i++)
        {
            var ingredient = _availableIngredients[Random.Range(0, _availableIngredients.Length)];
            
            if (newOrderLines.ContainsKey(ingredient.ingredientName))
                continue;
            
            var quantity = Random.Range(_minIngredientsPerOrderLine, _maxIngredientsPerOrderLine);
            var orderLine = new OrderLine(ingredient, quantity);
            newOrderLines.Add(ingredient.ingredientName, orderLine);
        }
        
        var newOrder = new Order(newOrderLines);

        return newOrder;
    }

    private void HandleCompletedOrder(int score)
    {
        CreateNewOrder();
    }
}
