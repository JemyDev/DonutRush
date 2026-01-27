using System.Collections.Generic;
using Components.Data;
using Components.SODatabase;
using UnityEngine;
using Random = UnityEngine.Random;
using Services.GameEventService;

/// <summary>
/// Handle order generation with a random pick of Scriptable Object ingredients
/// </summary>
public class OrderGenerator : MonoBehaviour
{
    private IngredientData[] _availableIngredients;

    // Current level parameters
    private int _maxIngredientsPerOrder;
    private int _minIngredientsPerOrderLine;
    private int _maxIngredientsPerOrderLine;

    private void Awake()
    {
        GameEventService.OnOrderCompleted += HandleCompletedOrder;
        GameEventService.OnOrderFailed += HandleFailedOrder;
        GameEventService.OnLevelChanged += HandleLevelChanged;
    }

    private void Start()
    {
        var baseParameters = ScriptableObjectDatabase.Get<LevelParametersData>("BaseLevelParameters");
        _maxIngredientsPerOrder = baseParameters.MaxIngredientsPerOrder;
        _minIngredientsPerOrderLine = baseParameters.MinIngredientsPerOrderLine;
        _maxIngredientsPerOrderLine = baseParameters.MaxIngredientsPerOrderLine;

        _availableIngredients = ScriptableObjectDatabase.GetAll<IngredientData>();

        CreateNewOrder();
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCompleted -= HandleCompletedOrder;
        GameEventService.OnOrderFailed -= HandleFailedOrder;
        GameEventService.OnLevelChanged -= HandleLevelChanged;
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

            if (newOrderLines.ContainsKey(ingredient.Name))
                continue;

            var quantity = Random.Range(_minIngredientsPerOrderLine, _maxIngredientsPerOrderLine + 1);
            var orderLine = new OrderLine(ingredient, quantity);
            newOrderLines.Add(ingredient.Name, orderLine);
        }

        return new Order(newOrderLines);
    }

    private void HandleCompletedOrder(int score)
    {
        CreateNewOrder();
    }

    private void HandleFailedOrder()
    {
        CreateNewOrder();
    }

    private void HandleLevelChanged(LevelParametersInfo newParameters)
    {
        _maxIngredientsPerOrder = newParameters.MaxIngredientsPerOrder;
        _minIngredientsPerOrderLine = newParameters.MinIngredientsPerOrderLine;
        _maxIngredientsPerOrderLine = newParameters.MaxIngredientsPerOrderLine;
    }
}
