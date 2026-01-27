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

        foreach (var orderLine in order.OrderLines)
        {
            CreateOrderLineUI(orderLine.Value);
        }

        _currentOrder = order;
        UpdateTotalCalories();
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
        orderLineUI.AddSprite(orderLine.Ingredient.Sprite);
    }
    
    private void UpdateTotalCalories()
    {
        _totalText.text = TotalOrderCalories.ToString();
    }
    
    private void HandleTotalCaloriesUpdate(IngredientData ingredient)
    {
        UpdateTotalCalories();
    }
}
