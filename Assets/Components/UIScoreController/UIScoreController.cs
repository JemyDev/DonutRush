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
        GameEventSystem.OnOrderCompleted += HandleOrderCompleted;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCompleted -= HandleOrderCompleted;
    }

    private void HandleOrderCompleted(int scoreToAdd)
    {
        SetScore(scoreToAdd);
    }
    
    private void SetScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        _scoreText.text = _currentScore.ToString();
    }
}
