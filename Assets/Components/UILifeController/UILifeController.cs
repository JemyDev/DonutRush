using Components.Data;
using UnityEngine;
using UnityEngine.UI;
using Components.SODatabase;
using Services.GameEventService;

/// <summary>
/// UI Life Controller to manage player's life points display
/// </summary>
public class UILifeController : MonoBehaviour
{
    [Header("UI Prefab")]
    [SerializeField] private Image _lifePointPrefab;

    private int _currentLife;

    private void Start()
    {
        var parameters = ScriptableObjectDatabase.Get<LevelParametersData>("BaseLevelParameters");
        InitializeLife(parameters.PlayerLife);
        GameEventService.OnPlayerLifeUpdated += UpdateLife;
    }

    private void OnDestroy()
    {
        GameEventService.OnPlayerLifeUpdated -= UpdateLife;
    }

    private void InitializeLife(int life)
    {
        _currentLife = life;

        for (var i = 0; i < life; i++)
        {
            Instantiate(_lifePointPrefab, transform);
        }
    }

    private void UpdateLife(int newLife)
    {
        var difference = _currentLife - newLife;

        if (difference > 0)
        {
            // Remove life icons from the end
            for (var i = 0; i < difference && transform.childCount > 0; i++)
            {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            }
        }
        else if (difference < 0)
        {
            // Add life icons
            for (var i = 0; i < -difference; i++)
            {
                Instantiate(_lifePointPrefab, transform);
            }
        }

        _currentLife = newLife;
    }
}
