using System.Collections.Generic;

public struct Order
{

    public Order(Dictionary<string, OrderLine> orderLines, int totalCalories)
    {
        OrderLines = new Dictionary<string, OrderLine>(orderLines);
        TotalCalories = totalCalories;
    }
    
    public Dictionary<string, OrderLine> OrderLines;
    public int TotalCalories;
}
