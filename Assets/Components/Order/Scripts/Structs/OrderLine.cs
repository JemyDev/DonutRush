public struct OrderLine
{
    public OrderLine(IngredientData ingredient, int quantity)
    {
        Ingredient = ingredient;
        Quantity = quantity;
    }

    public readonly IngredientData Ingredient;
    public int Quantity;
    public int TotalScoreByLine => Ingredient.Score * Quantity;
    
    public void DecreaseQuantity()
    {
        Quantity--;
        if (Quantity < 0)
            Quantity = 0;
    }
}
