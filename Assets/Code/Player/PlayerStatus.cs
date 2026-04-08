using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStatus : MonoBehaviour, IDataPersistence
{
    public float respawnTimer = 1;

    [SerializeField] GameObject deathMenu;
    [SerializeField] Animator fade_menu;

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

    public Vector3 respawnPoint = new Vector3(-8, 0, -5);

    private PlayerInput _playerInput;
    InputAction heal;

    [SerializeField] Animator vampyr;

    //public float cooldown = 8f;

    public AudioClip[] PlayerAudioClips;
    [Range(0, 1)] public float PlayerAudioVolume = 0.5f;

    public bool hitOnce = false;
    public bool hitTwice = false;

    public void LoadData(GameData data)
    {
        this.ammoPool = data.ammoPool;
        this.healthItemPool = data.healthItemPool;
        this.playerHealth = data.playerHealth;

        this.hasWeapon_2= data.hasWeapon_2;

        this.hasKey_1 = data.hasKey_1;
        this.hasKey_2 = data.hasKey_2;
        this.hasKey_3 = data.hasKey_3;
        this.hasKey_4 = data.hasKey_4;
    }

    public void SaveData(ref GameData data)
    {
        data.ammoPool = this.ammoPool;
        data.healthItemPool = this.healthItemPool;
        data.playerHealth = this.playerHealth;

        data.hasWeapon_2 = this.hasWeapon_2;

        data.hasKey_1 = this.hasKey_1;
        data.hasKey_2 = this.hasKey_2;
        data.hasKey_3 = this.hasKey_3;
        data.hasKey_4 = this.hasKey_4;
    }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        heal = _playerInput.actions.FindAction("Heal");
        _playerInput.enabled = true;
        fade_menu.SetBool("Start", false);
        AudioListener.pause = false;
    }
    

    private void Update()
    {
        if(heal.WasPerformedThisFrame())
        {
            AddHealth();
        } 
        /* if(playerHealth <= 0)
        {
            //this.transform.position = respawnPoint;
            //Invoke("Respawn", respawnTimer);
            deathMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            AudioListener.pause = true;

            
            //playerHealth = playerMaxHealth;
        } */

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
    private void Respawn()
    {
        playerHealth = playerMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        /* if(other.GetComponent<MonsterHealth>() != null)
        {
            if(playerHealth >= 1)
            {
                OnHit();
                playerHealth -= 1;
            }
            //if(playerHealth == 1)
            //{
                //OnHit();
                //playerHealth -= 1;
            //}
        } */
    }

    public void OnHit()
    {
        if (PlayerAudioClips.Length > 0)
            {
                var index = Random.Range(0, PlayerAudioClips.Length - 2);
                AudioSource.PlayClipAtPoint(PlayerAudioClips[index], transform.TransformPoint(this.transform.position), PlayerAudioVolume);
                
                
                if(vampyr.GetBool("IsAiming") == false)
                {
                    vampyr.Play("Damaged", 0, 0.25f);
                    _playerInput.enabled = false;
                    Invoke("Activate", 0.7f);
                }
                
            }
    }
    void Activate()
    {
        _playerInput.enabled = true;
    }
    public void OnDeath()
    {
        if (PlayerAudioClips.Length > 0)
            {
                var index = Random.Range(9, PlayerAudioClips.Length);
                AudioSource.PlayClipAtPoint(PlayerAudioClips[index], transform.TransformPoint(this.transform.position), PlayerAudioVolume);
                vampyr.Play("Death", 0, 0.25f);

                _playerInput.enabled = false;
                if (fade_menu.GetBool("Start") != true)
                {
                    fade_menu.SetBool("Start", true);
                }
                Invoke("Death", 5f);
            }
    }
    void Death()
    {
        _playerInput.enabled = true;
        deathMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = true;
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
