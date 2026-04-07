using UnityEngine;

public class BookLockedStatus : MonoBehaviour
{
    [SerializeField] doorBookcase _door1;
    [SerializeField] HUDManager _hud;
    public bool locked = true;
    public bool opened = false;
    public string whileLocked;
    public string whileUnlocked;
    public string whenOpening;
    public void LockedStatus1()
    {
        if (!locked)
        {
            if (!opened)
            {
                _hud.interactText = whenOpening;
                _hud.UpdateInteractText();
                _door1.Open1();
                opened = true;
            } else
            {
                _hud.interactText = whileUnlocked;
                _hud.UpdateInteractText();
            }
        } else
        {
            _hud.interactText = whileLocked;
            _hud.UpdateInteractText();
        }
    }    
        public void ChangeLocked()
    {
        locked = !locked;
    }

}
