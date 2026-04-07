using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync(1);
    }
    public void OnNewGameClicked()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(1);
    }
}
