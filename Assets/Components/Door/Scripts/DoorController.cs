using UnityEngine;
using Services.GameEventService;

/// <summary>
/// Door controller that updates Door prefab based on distributed ingredient
/// </summary>
public class DoorController : MonoBehaviour
{
    [Header("Ingredient Parameter")]
    [SerializeField] private IngredientData _ingredient;
    
    [Header("Door Prefab Parameters")]
    [SerializeField] private MeshRenderer[] _wallsMeshRenderer;
    [SerializeField] private Transform _doorTrigger;

    private void Awake()
    {
        GameEventService.OnIngredientSpawned += AttachIngredient;
        GameEventService.OnPlayerTriggerDoorPassed += HandleDoorPassed;
    }

    private void OnDestroy()
    {
        GameEventService.OnIngredientSpawned -= AttachIngredient;
        GameEventService.OnPlayerTriggerDoorPassed -= HandleDoorPassed;
    }
    
    private void AttachIngredient(IngredientData ingredient)
    {
        if (_ingredient) return;
        
        _ingredient = ingredient;
        UpdateWallsMaterial();
    }
    
    private void HandleDoorPassed(Collider doorReference)
    {
        if (doorReference.gameObject == _doorTrigger.gameObject)
        {
            GameEventService.OnIngredientCollected?.Invoke(_ingredient);
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
