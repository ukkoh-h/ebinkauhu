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
        SceneManager.LoadSceneAsync(1);
        DataPersistenceManager.instance.LoadGame();
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    public void OnNewGameClicked()
    {
        SceneManager.LoadSceneAsync(1);
        DataPersistenceManager.instance.NewGame();
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}
