using UnityEngine;

/// <summary>
/// Door controller that updates Door prefab based on distributed ingredient
/// </summary>
public class DoorController : MonoBehaviour
{
    [Header("Ingredient Parameter")]
    [SerializeField] private IngredientScriptableObject _ingredient;
    
    [Header("Door Prefab Parameters")]
    [SerializeField] private MeshRenderer[] _wallsMeshRenderer;
    [SerializeField] private Transform _doorTrigger;

    private void Awake()
    {
        GameEventSystem.OnIngredientDistributed += AttachIngredient;
        GameEventSystem.OnDoorPassed += HandleDoorPassed;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnIngredientDistributed -= AttachIngredient;
        GameEventSystem.OnDoorPassed -= HandleDoorPassed;
    }
    
    private void AttachIngredient(IngredientScriptableObject ingredient)
    {
        if (_ingredient) return;
        
        _ingredient = ingredient;
        UpdateWallsMaterial();
    }
    
    private void HandleDoorPassed(Collider doorReference)
    {
        if (doorReference.gameObject == _doorTrigger.gameObject)
        {
            GameEventSystem.OnIngredientCollected?.Invoke(_ingredient);
        }
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
