using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{
    private CutsceneElementBase[] cutsceneElements;
    private int index = -1;

    public void Start()
    {
        cutsceneElements = GetComponents<CutsceneElementBase>();
    }

    private void ExecuteCurrentElement()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(index >= 0 && index < cutsceneElements.Length)
        {
            cutsceneElements[index].Execute();
        } 
        else if(scene.name == $"Main Menu")
        {   
            DataPersistenceManager.instance.NewGame();
            SceneManager.LoadSceneAsync(1);
            
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
            
            
        
    }

    public void PlayNextElement()
    {
        index++;
        ExecuteCurrentElement();
    }
}
