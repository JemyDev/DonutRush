using TMPro;
using UnityEngine;
using Services.GameEventService;
using Services.SaveService;

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
        GameEventService.OnOrderCompleted += HandleOrderCompleted;
        GameEventService.OnGameOverState += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCompleted -= HandleOrderCompleted;
        GameEventService.OnGameOverState -= HandleGameOver;
    }

    private void HandleOrderCompleted(int scoreToAdd)
    {
        SetScore(scoreToAdd);
        UpdateScoreData();
    }

    private void HandleGameOver(bool enterState)
    {
        if (!enterState) return;
        // Save total high score
        UpdateScoreData();
    }

    private void UpdateScoreData()
    {
        
        if (!SaveService.TryLoad(out var saveData))
        {
            saveData = new SaveData();
        }
        
        if (saveData.HighScore < _currentScore)
        {
            saveData.HighScore = _currentScore;
            SaveService.Save(saveData);
        }
    }
    
    private void SetScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        _scoreText.text = _currentScore.ToString();
    }
}
