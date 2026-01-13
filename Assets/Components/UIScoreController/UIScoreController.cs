using TMPro;
using UnityEngine;

/// <summary>
/// Handle the UI Score display and updates regarding current ingredient collected
/// </summary>
public class UIScoreController : MonoBehaviour
{
    [Header("UI Element")]
    [SerializeField] private TMP_Text _scoreText;

    private int _currentScore = 0;
    
    private void Start()
    {
        SetScore(_currentScore);
        GameEventSystem.OnIngredientCollected += HandleIngredientCollected;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnIngredientCollected -= HandleIngredientCollected;
    }

    private void HandleIngredientCollected(IngredientScriptableObject ingredient)
    {
        SetScore(ingredient.ingredientScore);
    }
    
    private void SetScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        _scoreText.text = _currentScore.ToString();
    }
}
