using TMPro;
using UnityEngine;
using Services.SaveService;

public class UIScoreBoardController : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalRunsText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _totalIngredientsCollectedText;
    [SerializeField] private TMP_Text _totalOrdersCompletedText;

    private SaveData _saveData;
    
    private void Start()
    {
        if (!SaveService.TryLoad(out _saveData))
        {
            _saveData = new SaveData();
        }

        UpdateTotalRuns();
        UpdateHighScore();
        UpdateTotalIngredientsCollected();
        UpdateTotalOrdersCompleted();
    }
    
    private void UpdateTotalRuns()
    {
        _totalRunsText.text = _saveData.RunCount.ToString();
    }
    
    private void UpdateHighScore()
    {
        _highScoreText.text = _saveData.HighScore.ToString();
    }
    
    private void UpdateTotalIngredientsCollected()
    {
        _totalIngredientsCollectedText.text = _saveData.TotalIngredientsCollected.ToString();
    }
    
    private void UpdateTotalOrdersCompleted()
    {
        _totalOrdersCompletedText.text = _saveData.TotalOrdersCompleted.ToString();
    }
}
