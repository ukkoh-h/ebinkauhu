using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryMenu : MonoBehaviour
{
    private InputMaster playerControls;
    private InputAction inventory;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private bool isPaused;

    void Awake()
    {
        playerControls = new InputMaster();
    }

    private void OnEnable()
    {
        inventory = playerControls.UI.InventoryOpenClose;
        inventory.Enable();

        inventory.performed += Pause;
    }
    private void OnDisable()
    {
        inventory.Disable();
    }

    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else{
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        inventoryUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
