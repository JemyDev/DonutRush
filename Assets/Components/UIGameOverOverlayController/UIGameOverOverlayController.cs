using UnityEngine;
using Services.GameEventService;
using Services.SceneLoaderService;

public class UIGameOverOverlayController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    
    private void Start()
    {
        GameEventService.OnGameOverState += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameOverState -= HandleGameOver;
    }
    
    private void HandleGameOver(bool enterState)
    {
        _gameOverUI.SetActive(enterState);
    }

    public void RestartGame()
    {
        SceneLoaderService.LoadLevel();
    }

    public void BackToMainMenu()
    {
        SceneLoaderService.LoadMainMenu();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
