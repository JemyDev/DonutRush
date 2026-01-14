using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Order controller that updates the order UI based on current order
/// </summary>
public class UIOrderController : MonoBehaviour
{
    [Header("UI Prefab")]
    [SerializeField] private UIOrderLineController _orderLinePrefab;
    
    private void Awake()
    {
        GameEventSystem.OnOrderCreated += HandleOrderCreated;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCreated -= HandleOrderCreated;
    }

    private void HandleOrderCreated(Dictionary<string, OrderLine> orderLines)
    {
        ClearExistingOrderLines();

        foreach (var orderLine in orderLines)
        {
            CreateOrderLineUI(orderLine.Value);
        }
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
}
