public struct OrderLine
{
    public OrderLine(IngredientScriptableObject ingredient, int quantity)
    {
        Ingredient = ingredient;
        Quantity = quantity;
    }

    public IngredientScriptableObject Ingredient;
    public int Quantity;
}
