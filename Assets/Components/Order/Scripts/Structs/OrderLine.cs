using UnityEngine;

public struct OrderLine
{
    public OrderLine(IngredientData ingredient, int quantity)
    {
        Ingredient = ingredient;
        Quantity = quantity;
    }

    public IngredientData Ingredient { get; }
    public int Quantity { get; private set; }
    public int TotalScoreByLine => Ingredient.Score * Quantity;

    public void DecreaseQuantity()
    {
        Quantity = Mathf.Max(0, Quantity - 1);
    }
}
