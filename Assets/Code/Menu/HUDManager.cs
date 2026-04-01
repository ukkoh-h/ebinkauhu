using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HUDManager : MonoBehaviour
{
    public Weapon weapon;
    public PlayerStatus playerStatus;

    public TextMeshProUGUI text_ammo;
    public TextMeshProUGUI text_ammo_pool;
    public TextMeshProUGUI text_health;
    public TextMeshProUGUI text_heal_pool;

    public TextMeshProUGUI text_interact;

    void Start()
    {
        UpdateHealthText();
        UpdateAmmoText();

        UpdateInteractText();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
        UpdateAmmoText();
        
    }

    public void UpdateInteractText()
    {
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
            text_health.text = $"♥♥♥♥♥♥";
        } 
        else if (playerStatus.playerHealth == 5)
        {
            text_health.text = $"♥♥♥♥♥";
        }
        else if (playerStatus.playerHealth == 4)
        {
            text_health.text = $"♥♥♥♥";
        }
        else if (playerStatus.playerHealth== 3)
        {
            text_health.text = $"♥♥♥";
        }
        else if (playerStatus.playerHealth == 2)
        {
            text_health.text = $"♥♥";
        } 
        else if (playerStatus.playerHealth == 1)
        {
            text_health.text = $"♥";
        } 
        else
        {
            text_health.text = $"";
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
}