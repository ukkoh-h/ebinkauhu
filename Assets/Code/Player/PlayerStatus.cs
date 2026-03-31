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
    public int playerMaxHealth = 6;
    public int maxHealthItems = 15;
    public int maxPlayerAmmo = 300;

    //Potionin healaama määrä, healaus
    public int potionHealAmount = 3;

    private bool healing;
    //private float healTime;
    //private float healCooldown;

    //Tämänhetkinen health, heal item määrä, ammo määrä.
    public int playerHealth = 6;
    public int healthItemPool = 5;
    public int ammoPool = 25;

    private PlayerInput _playerInput;
    InputAction heal;

    //public float cooldown = 8f;

    public AudioClip[] PlayerAudioClips;
    [Range(0, 1)] public float PlayerAudioVolume = 0.5f;

    public bool hitOnce = false;
    public bool hitTwice = false;

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

        /* if(playerHealth == (playerMaxHealth / 3) * 2 )
        {
            if(hitOnce == false) 
            {
                OnHit();
                hitOnce = true;
            }
        }
        
        if(playerHealth == playerMaxHealth / 3)
        {
            if(hitTwice == false) 
            {
                OnHit();
                hitTwice = true;
            }
        } */

        /* if(playerHealth <= 0)
        {
            OnHit();
            //gameObject.SetActive(false);
            //Invoke("ActivatePlayer", cooldown);
        } */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MonsterHealth>() != null)
        {
            if(playerHealth >= 1)
            {
                OnHit();
                playerHealth -= 1;
            }
            /* if(playerHealth == 1)
            {
                OnHit();
                playerHealth -= 1;
            } */

        }
            

    }

    private void OnHit()
    {
        if (PlayerAudioClips.Length > 0)
            {
                var index = Random.Range(0, PlayerAudioClips.Length);
                AudioSource.PlayClipAtPoint(PlayerAudioClips[index], transform.TransformPoint(this.transform.position), PlayerAudioVolume);
            }
    }

    void ActivatePlayer()
    {
        gameObject.SetActive(true);
        playerHealth = playerMaxHealth;
        //hitOnce = false;
        //hitTwice = false;
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
