using UnityEngine;
using Services.SaveService;
using Services.SceneLoaderService;

public class UIMainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Save total runs
        ProgressService.RecordRunCount();
        SceneLoaderService.LoadLevel();
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
