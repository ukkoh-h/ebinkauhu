using UnityEngine;

public class lockedInteractable : MonoBehaviour
{
    [SerializeField] lockedStatus lockStat;
    [SerializeField] BookLockedStatus bookLockStat;
    [SerializeField] GameObject monster;
    bool interactable = true;
    public void Interact()
    {
        if (interactable)
        {
            bookLockStat?.ChangeLocked();
            lockStat?.ChangeLocked();
            monster?.SetActive(true);
            interactable = !interactable;
            Debug.Log("something unlocked");
        }
    }
}
