public struct OrderLine
{
    public OrderLine(IngredientScriptableObject ingredient, int quantity)
    {
        Ingredient = ingredient;
        Quantity = quantity;
    }

    public readonly IngredientScriptableObject Ingredient;
    public int Quantity;
    public int TotalScoreByLine => Ingredient.ingredientScore * Quantity;
    
    public void DecreaseQuantity()
    {
        Quantity--;
        if (Quantity < 0)
            Quantity = 0;
    }
}
