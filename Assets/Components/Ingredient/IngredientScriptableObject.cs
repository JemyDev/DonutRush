using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients/Ingredient")]
public class IngredientScriptableObject : ScriptableObject
{
    public Color ingredientColor;
    public int ingredientScore;
    public string ingredientName;
    public Sprite ingredientSprite;
    public GameObject ingredientPrefab;
    public Material ingredientMaterial;
}
