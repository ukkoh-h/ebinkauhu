using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUDManager : MonoBehaviour
{
    public Weapon weapon;
    public PlayerStatus playerStatus;

    [SerializeField] Animator blood_less;
    [SerializeField] Animator blood_more;

    public TextMeshProUGUI text_ammo;
    public TextMeshProUGUI text_ammo_pool;
    public TextMeshProUGUI text_heal_pool;

    public TextMeshProUGUI text_interact;
    public string interactText;

    void Start()
    {
        UpdateHealthText();
        UpdateAmmoText();

        UpdateInteractText();
        blood_less.SetBool("Blood_1", false);
        blood_more.SetBool("Blood_2", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
        UpdateAmmoText();
    }

    public void UpdateInteractText()
    {
        text_interact.text = $"{interactText}";
        StartCoroutine(TextTimerCorutine());
        //text_interact.text = $"";
        /* if (!raycasthit) text_interact.text = $"";

        if (raycasthit)
        {
            text_interact.text = $"Press E To Interact.";
        } 
        if (raycasthit)
        {
            text_interact.text = $"Door Is Locked.";
            yield return new WaitForSeconds (2f);
            text_interact.text = $"";
        } 
        if (raycasthit)
        {
            text_interact.text = $"Door Unlocked.";
            yield return new WaitForSeconds (2f);
            text_interact.text = $"";
        } 
        if (raycasthit)
        {
            text_interact.text = $"{AmmoPickup.ammoPickupAmount} Bolt(s)";
            yield return new WaitForSeconds (2f);
            text_interact.text = $"";
        } 
        if (raycasthit)
        {
            text_interact.text = $"{HealthPickup.healthPickupAmount} Potion(s)";
            yield return new WaitForSeconds (2f);
            text_interact.text = $"";
        }  */
    }

    public void UpdateHealthText()
    {       
        if (playerStatus.playerHealth == 6)
        {
            if (blood_less.GetBool("Blood_1") != false)
            {
                blood_less.SetBool("Blood_1", false);
            }
            if (blood_more.GetBool("Blood_2") != false)
            {
                blood_more.SetBool("Blood_2", false);
            }
        } 
        else if (playerStatus.playerHealth == 5)
        {
            if (blood_less.GetBool("Blood_1") != false)
            {
                blood_less.SetBool("Blood_1", false);
            }
            if (blood_more.GetBool("Blood_2") != false)
            {
                blood_more.SetBool("Blood_2", false);
            }
        }
        else if (playerStatus.playerHealth == 4)
        {
            if (blood_less.GetBool("Blood_1") != true)
            {
                blood_less.SetBool("Blood_1", true);
            }
            if (blood_more.GetBool("Blood_2") != false)
            {
                blood_more.SetBool("Blood_2", false);
            }
        }
        else if (playerStatus.playerHealth== 3)
        {
            if (blood_less.GetBool("Blood_1") != true)
            {
                blood_less.SetBool("Blood_1", true);
            }
            if (blood_more.GetBool("Blood_2") != false)
            {
                blood_more.SetBool("Blood_2", false);
            }
        }
        else if (playerStatus.playerHealth == 2)
        {
            if (blood_less.GetBool("Blood_1") != false)
            {
                blood_less.SetBool("Blood_1", false);
            }
            if (blood_more.GetBool("Blood_2") != true)
            {
                blood_more.SetBool("Blood_2", true);
            }
        } 
        else if (playerStatus.playerHealth == 1)
        {
            if (blood_less.GetBool("Blood_1") != false)
            {
                blood_less.SetBool("Blood_1", false);
            }
            if (blood_more.GetBool("Blood_2") != true)
            {
                blood_more.SetBool("Blood_2", true);
            }
        } 
        else
        {
            if (blood_less.GetBool("Blood_1") != false)
            {
                blood_less.SetBool("Blood_1", false);
            }
            if (blood_more.GetBool("Blood_2") != false)
            {
                blood_more.SetBool("Blood_2", false);
            }
        }
        text_heal_pool.text = $"Health item pool: {playerStatus.healthItemPool}";
    }

    public void UpdateAmmoText()
    {
        if (weapon.ammoLeft == 5)
        {
            text_ammo.text = $"↑↑↑↑↑";
        }
        else if (weapon.ammoLeft == 4)
        {
            text_ammo.text = $"↑↑↑↑";
        }
        else if (weapon.ammoLeft == 3)
        {
            text_ammo.text = $"↑↑↑";
        }
        else if (weapon.ammoLeft == 2)
        {
            text_ammo.text = $"↑↑";
        } 
        else if (weapon.ammoLeft == 1)
        {
            text_ammo.text = $"↑";
        } 
        else
        {
            text_ammo.text = $"";
        }
        text_ammo_pool.text = $"Ammo pool: {playerStatus.ammoPool}";
    }

    private IEnumerator TextTimerCorutine()
    {
        yield return new WaitForSeconds(2f);
        text_interact.text = $"";
    }
}