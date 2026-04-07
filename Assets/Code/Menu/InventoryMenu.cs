using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
    private InputMaster playerControls;
    private InputAction inventory;

    public PlayerStatus playerStatus;
    public Weapon weapon;
    public TextMeshProUGUI text_health;
    public TextMeshProUGUI text_ammo_pool;
    public TextMeshProUGUI text_heal_pool;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private bool isPaused;

    void Awake()
    {
        playerControls = new InputMaster();
    }
    void Update()
    {
        UpdateHealthText();
        UpdateAmmoText();
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
    public void UpdateAmmoText()
    {
        text_ammo_pool.text = $"Use to reload weapon. No. loaded/held: {weapon.ammoLeft}/{playerStatus.ammoPool}";
        
    }
    public void UpdateHealthText()
    {       
        if(playerStatus.playerHealth > 4)
        {
            text_health.text = $"Fine";
        }
        else if(playerStatus.playerHealth > 2)
        {
            text_health.text = $"Caution";
        }
        else
        {
            text_health.text = $"Danger";
        }

        text_heal_pool.text = $"Use to heal player. No. held: {playerStatus.healthItemPool}";
    }
    public void OnHealItemUse()
    {
        playerStatus.AddHealth();
    }
    public void OnAmmoItemUse()
    {
        if(weapon.ammoLeft == 5)
        {
            //weapon.MagEmpty(); UI sound
        } 
        else
        {
            weapon.Reload();
        }
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
