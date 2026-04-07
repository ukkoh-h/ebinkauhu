//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class lockedInteractable : MonoBehaviour
{
    [SerializeField] lockedStatus lockStat;
    [SerializeField] BookLockedStatus bookLockStat;
    [SerializeField] GameObject monster;
    //public GameObject gameObject;
    bool interactable = true;
    public void Interact()
    {
        if (interactable)
        {
            bookLockStat?.ChangeLocked();
            lockStat?.ChangeLocked();
            if (monster != null) monster.SetActive(true);
            interactable = !interactable;
            gameObject.SetActive(false);
        }
    }
}
