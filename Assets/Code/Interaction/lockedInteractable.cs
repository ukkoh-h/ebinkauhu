using UnityEngine;

public class lockedInteractable : MonoBehaviour
{
    [SerializeField] lockedStatus lockStat;
    bool interactable = true;
    public void Interact()
    {
        if (interactable)
        {
            lockStat.ChangeLocked();
            interactable = !interactable;
        }
    }
}
