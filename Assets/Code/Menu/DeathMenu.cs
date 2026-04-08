using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void OnLoadGameClicked()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync(1);
        AudioListener.pause = false;
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}
