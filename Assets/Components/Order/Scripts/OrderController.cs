using UnityEngine;

/// <summary>
/// Handle current order and updates it
/// </summary>
public class OrderController : MonoBehaviour, IDataService
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
    
    private void HandleOrderUpdate(IngredientScriptableObject ingredient)
    {
        UpdateOrderLine(ingredient.ingredientName);
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
            var saveData = GetSaveData();
            saveData.TotalIngredientsCollected++;
            Save(saveData);
        }
        
        if (IsOrderCompleted && _currentOrder.OrderLines.Count > 0)
        {
            _currentOrder.OrderLines.Clear();
            GameEventService.OnOrderCompleted?.Invoke(_scoreToAdd);
        }
    }

    public SaveData GetSaveData()
    {
        if (!SaveService.TryLoad(out var saveData))
        {
            saveData = new SaveData();
        }

        return saveData;
    }

    public void Save(SaveData saveData)
    {
        SaveService.Save(saveData);
    }
}
