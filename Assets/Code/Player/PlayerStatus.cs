using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour
{
    public bool hasWeapon_1;
    public bool hasWeapon_2;

    public bool hasKey_1;
    public bool hasKey_2;
    public bool hasKey_3;
    public bool hasKey_4;

    //Max arvot healthille, heal itemeille, ammolle.
    public int playerMaxHealth;
    public int maxHealthItems;
    public int maxPlayerAmmo;

    //Potionin healaama määrä, healaus
    public int potionHealAmount;

    private bool healing;
    //private float healTime;
    //private float healCooldown;

    private PlayerInput _playerInput;
    InputAction heal;

    //Tämänhetkinen health, heal item määrä, ammo määrä.
    public int playerHealth;
    public int healthItemPool;
    public int ammoPool;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        heal = _playerInput.actions.FindAction("Heal");
    }
    

    private void Update()
    {
        if(heal.WasPerformedThisFrame())
        {
            AddHealth();
        } 
           
    }
    
    public void AddHealth()
    {
        if(healthItemPool > 0)
        {
            playerHealth += potionHealAmount;
            if(playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            healthItemPool--;
        }
        else
        {
            //AudioSource.PlayClipAtPoint(WeaponAudioClips[0], transform.TransformPoint(_controller.center), WeaponAudioVolume);
        }
        
    }
    public void AddPotions(int healthPickupAmount)
    {
        healthItemPool += healthPickupAmount;
        if(healthItemPool > maxHealthItems)
        {
            healthItemPool = maxHealthItems;
        }
    }
    //Oven avaus esim
    /* [SerializeField] bool isLocked; 

    public bool TryOpenDoor(Inventory playerInventory)
    {
        if(isLocked && playerInventory.hasKey_1 == false)
        {
            return false;
        }

        return true;
    }

    */
}
