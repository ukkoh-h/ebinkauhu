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
    public TextMeshProUGUI text_interact;
    public string interactText;

    void Start()
    {
        UpdateHealthText();

        UpdateInteractText();
        blood_less.SetBool("Blood_1", false);
        blood_more.SetBool("Blood_2", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
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
        if (playerStatus.playerHealth >= 5)
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
        else if (playerStatus.playerHealth >= 3)
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
        else if (playerStatus.playerHealth >= 1)
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
    }

    private IEnumerator TextTimerCorutine()
    {
        yield return new WaitForSeconds(3f);
        text_interact.text = $"";
    }
}