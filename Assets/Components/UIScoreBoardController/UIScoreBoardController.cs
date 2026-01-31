using TMPro;
using UnityEngine;
using Services.SaveService;

public class UIScoreBoardController : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalRunsText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _totalIngredientsCollectedText;
    [SerializeField] private TMP_Text _totalOrdersCompletedText;

    private PlayerProgress _playerProgress;
    
    private void Start()
    {
        UpdateTotalRuns();
        UpdateHighScore();
        UpdateTotalIngredientsCollected();
        UpdateTotalOrdersCompleted();
    }
    
    private void UpdateTotalRuns()
    {
        _totalRunsText.text = ProgressService.CurrentProgress.RunCount.ToString();
    }
    
    private void UpdateHighScore()
    {
        _highScoreText.text = ProgressService.CurrentProgress.HighScore.ToString();
    }
    
    private void UpdateTotalIngredientsCollected()
    {
        _totalIngredientsCollectedText.text = ProgressService.CurrentProgress.TotalIngredientsCollected.ToString();
    }
    
    private void UpdateTotalOrdersCompleted()
    {
        _totalOrdersCompletedText.text = ProgressService.CurrentProgress.TotalOrdersCompleted.ToString();
    }
}
