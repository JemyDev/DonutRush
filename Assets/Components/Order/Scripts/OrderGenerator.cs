using System.Collections.Generic;
using Components.Data;
using Components.SODatabase;
using UnityEngine;
using Random = UnityEngine.Random;
using Services.GameEventService;
using Services.SaveService;

/// <summary>
/// Handle order generation with a random pick of Scriptable Object ingredients
/// </summary>
public class OrderGenerator : MonoBehaviour
{
    private IngredientData[] _availableIngredients;
    private LevelParametersData _levelParametersData;

    private void Awake()
    {
        GameEventService.OnOrderCompleted += HandleCompletedOrder;
    }

    private void Start()
    {
        // Load level parameters
        var levelIndex = 1;
        if (SaveService.TryLoad(out var saveData))
        {
            levelIndex = saveData.LevelIndex;
        }
        _levelParametersData = ScriptableObjectDatabase.Get<LevelParametersData>("Level" + levelIndex);
        _availableIngredients = ScriptableObjectDatabase.GetAll<IngredientData>();
        
        Debug.Log(_availableIngredients);
        
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
        
        for (var i = 0; i < _levelParametersData.MaxIngredientsPerOrder; i++)
        {
            var ingredient = _availableIngredients[Random.Range(0, _availableIngredients.Length)];
            
            if (newOrderLines.ContainsKey(ingredient.Name))
                continue;
            
            var quantity = Random.Range(_levelParametersData.MinIngredientsPerOrderLine, _levelParametersData.MaxIngredientsPerOrderLine);
            var orderLine = new OrderLine(ingredient, quantity);
            newOrderLines.Add(ingredient.Name, orderLine);
        }
        
        var newOrder = new Order(newOrderLines);

        return newOrder;
    }

    private void HandleCompletedOrder(int score)
    {
        CreateNewOrder();
    }
}
