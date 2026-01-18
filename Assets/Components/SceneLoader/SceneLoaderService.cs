using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoaderService
{
    public static void LoadLevel()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        SceneManager.LoadScene("LevelUI", LoadSceneMode.Additive);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
