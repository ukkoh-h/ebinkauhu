//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class lockedInteractable : MonoBehaviour
{
    [SerializeField] lockedStatus lockStat;
    [SerializeField] BookLockedStatus bookLockStat;
    [SerializeField] GameObject monster;
    public roomTrigger roomTrigger;
    bool interactable = true;
    public bool finalScene;
    public void Interact()
    {
        if (interactable)
        {
            bookLockStat?.ChangeLocked();
            lockStat?.ChangeLocked();
            if (monster != null) monster.SetActive(true);
            if (finalScene) roomTrigger.finalScene = true;
            interactable = !interactable;
            gameObject.SetActive(false);
        }
    }
}
