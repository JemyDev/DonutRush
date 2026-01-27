using TMPro;
using UnityEngine;
using Services.GameEventService;

/// <summary>
/// Handle the UI Score display
/// </summary>
public class UIScoreController : MonoBehaviour
{
    [Header("UI Element")]
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        DisplayScore(0);
        GameEventService.OnScoreUpdated += DisplayScore;
    }

    private void OnDestroy()
    {
        GameEventService.OnScoreUpdated -= DisplayScore;
    }

    private void DisplayScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
