using UnityEngine;

public class MenuManager : MonoBehaviour
{
    /* [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerAttack _playerAttack;

    private bool isPaused;

    private void Start()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput)
        {
            if(!isPaused)
            {
                Pause();
            }
            {
                Unpause();
            }
        }
    }

    #region Pause/Unpause Functions

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        _player.enabled = false;
        _playerAttack.enabled = false;

        OpenMainMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _player.enabled = true;
        _playerAttack.enabled = true;

        CloseAllMenus();
    }
    #endregion

    #region Canvas Activations/Deactivations

    private void OpenMainMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);
    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
    }
    #endregion

    #region Main Menu Button Actions

    public void OnSettingsPress()
    {

    }
    #endregion */
}
