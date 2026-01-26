using System.Collections.Generic;
using System.Linq;

public readonly struct Order
{
    public Order(Dictionary<string, OrderLine> orderLines)
    {
        OrderLines = new Dictionary<string, OrderLine>(orderLines);
    }
    
    public readonly Dictionary<string, OrderLine> OrderLines;
    public int TotalCalories => OrderLines.Values.Sum(orderLine => orderLine.TotalScoreByLine);
}
