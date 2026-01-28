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
        UpdateTotalRuns();
        UpdateHighScore();
        UpdateTotalIngredientsCollected();
        UpdateTotalOrdersCompleted();
    }
    
    private void UpdateTotalRuns()
    {
        _totalRunsText.text = SaveDataService.RunCount.ToString();
    }
    
    private void UpdateHighScore()
    {
        _highScoreText.text = SaveDataService.HighScore.ToString();
    }
    
    private void UpdateTotalIngredientsCollected()
    {
        _totalIngredientsCollectedText.text = SaveDataService.TotalIngredientsCollected.ToString();
    }
    
    private void UpdateTotalOrdersCompleted()
    {
        _totalOrdersCompletedText.text = SaveDataService.TotalOrdersCompleted.ToString();
    }
}
