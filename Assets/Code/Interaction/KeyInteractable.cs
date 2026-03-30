using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] lockedStatus _doorLocked;
    bool interactable = true;
    public void Interact()
    {
        if (interactable)
        {
            _doorLocked.ChangeLocked();
            interactable = !interactable;
            Debug.Log("DOOR'S UNLOCKED!");
        }
    }
}

