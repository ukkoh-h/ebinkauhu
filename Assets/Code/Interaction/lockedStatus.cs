using UnityEngine;

public class lockedStatus : MonoBehaviour
{
    [SerializeField] door2 _door1;
    [SerializeField] door2 _door2;
    [SerializeField] lockedInteractable _lockedInt;
    [SerializeField] HUDManager _hud;
    public bool locked1 = false;
    public bool locked2 = false;
    public bool locked3 = false;
    public bool lockedForPlayer = false;
    bool firstTime = false;
    public string whileLocked;
    public string whileUnlocked;
    public string whenOpening;

    public void LockedStatus1()
    {
        if (!locked1 && !locked2 && !locked3 && !firstTime)
        {
            _hud.interactText = whileUnlocked;
            _hud.UpdateInteractText();
            _door1?.Open1();
            _door2?.Open2();
            _lockedInt?.Interact();
            firstTime = true;
        }
        else if (!locked1 && !locked2 && !locked3 && firstTime)
        {
            _hud.interactText = whenOpening;
            _hud.UpdateInteractText();
            _door1?.Open1();
            _door2?.Open2();
            _lockedInt?.Interact();
        }
        else
        {
            _hud.interactText = whileLocked;
            _hud.UpdateInteractText();
            //Debug.Log("IT'S LOCKED!");
        }
    }    
    public void LockedStatus2()
    {
        if (!locked1 && !locked2 && !locked3 && !firstTime)
        {
            _hud.interactText = whileUnlocked;
            _hud.UpdateInteractText();
            _door1?.Open2();
            _door2?.Open1();
        } else if (!locked1 && !locked2 && !locked3 && firstTime)
        {
            _hud.interactText = whenOpening;
            _hud.UpdateInteractText();
            _door1?.Open1();
            _door2?.Open2();
            firstTime = false;
        } 
        else
        {
            _hud.interactText = whileLocked;
            _hud.UpdateInteractText();
        }
    }
    public void LockedStatus3()
    {
        if (!locked1 && !locked2 && !locked3)
        {
            _door1?.Open1();
            _door2?.Open2();
        }
    }
    public void LockedStatus4()
    {
        if (!locked1 && !locked2 && !locked3)
        {
            _door1?.Open2();
            _door2?.Open1();
        }
    }
    public void ChangeLocked()
    {
        if (locked1)
        {
            locked1 = false;
        } else if (locked2)
        {
            locked2 = false;
        } else if (locked3)
        {
            locked3 = false;
            Debug.Log("IT'S UNLOCKED!");
        }
    }
    public void MonsterChangeLocked()
    {
        if (lockedForPlayer)
        {
            locked1 = !locked1;
        }
    }
}
