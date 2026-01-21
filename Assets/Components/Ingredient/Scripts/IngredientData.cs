using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Ingredients/IngredientData")]
public class IngredientData : ScriptableObject
{
    public Color ingredientColor;
    public int ingredientScore;
    public string ingredientName;
    public Sprite ingredientSprite;
    public GameObject ingredientPrefab;
    public Material ingredientMaterial;
}
