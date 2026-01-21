using TMPro;
using UnityEngine;
using Services.GameEventService;
using Services.SaveService;

/// <summary>
/// Handle the UI Score display and updates regarding current ingredient collected
/// </summary>
public class UIScoreController : MonoBehaviour, IDataService
{
    [Header("UI Element")]
    [SerializeField] private TMP_Text _scoreText;

    private int _currentScore = 0;
    
    private void Start()
    {
        SetScore(_currentScore);
        GameEventService.OnOrderCompleted += HandleOrderCompleted;
        GameEventService.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCompleted -= HandleOrderCompleted;
        GameEventService.OnGameOver -= HandleGameOver;
    }

    private void HandleOrderCompleted(int scoreToAdd)
    {
        SetScore(scoreToAdd);
        UpdateScoreData();
    }

    private void HandleGameOver()
    {
        // Save total high score
        UpdateScoreData();
    }

    private void UpdateScoreData()
    {
        var saveData = GetSaveData();
        if (saveData.HighScore < _currentScore)
        {
            saveData.HighScore = _currentScore;
            Save(saveData);
        }
    }
    
    private void SetScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        _scoreText.text = _currentScore.ToString();
    }

    public SaveData GetSaveData()
    {
        // Save total high score
        if (!SaveService.TryLoad(out var saveData))
        {
            saveData = new SaveData();
        }

        return saveData;
    }

    public void Save(SaveData saveData)
    {
        SaveService.Save(saveData);
    }
}
