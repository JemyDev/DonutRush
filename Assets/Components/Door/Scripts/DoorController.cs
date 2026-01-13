using UnityEngine;

/// <summary>
/// Door controller that updates Door prefab based on distributed ingredient
/// </summary>
public class DoorController : MonoBehaviour
{
    [SerializeField] private IngredientScriptableObject _ingredient;
    [SerializeField] private MeshRenderer[] _wallsMeshRenderer;

    private void Awake()
    {
        GameEventSystem.OnIngredientDistributed += AttachIngredient;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnIngredientDistributed -= AttachIngredient;
    }
    
    private void AttachIngredient(IngredientScriptableObject ingredient)
    {
        if (_ingredient) return;
        
        _ingredient = ingredient;
        UpdateWallsMaterial();
    }
    
    private void UpdateWallsMaterial()
    {
        if (!_ingredient) return;
        
        foreach (var wall in _wallsMeshRenderer)
        {
            wall.material = _ingredient.ingredientMaterial;
        }
    }
}
