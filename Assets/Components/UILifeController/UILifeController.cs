using Components.Data;
using UnityEngine;
using UnityEngine.UI;
using Components.SODatabase;
using Services.GameEventService;
using Services.SaveService;

/// <summary>
/// UI Life Controller to manage player's life points display
/// </summary>
public class UILifeController : MonoBehaviour
{
    [Header("UI Prefab")]
    [SerializeField] private Image _lifePointPrefab;

    private void Start()
    {
        var parameters = ScriptableObjectDatabase.Get<LevelParametersData>("BaseLevelParameters");
        SetLife(parameters.PlayerLife);
        GameEventService.OnPlayerLifeUpdated += SetLife;
    }

    private void OnDestroy()
    {
        GameEventService.OnPlayerLifeUpdated -= SetLife;
    }

    private void SetLife(int life)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        for (var i = 0; i < life; i++)
        {
            Instantiate(_lifePointPrefab, transform);
        }
    }
}
