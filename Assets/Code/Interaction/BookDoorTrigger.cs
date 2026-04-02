using UnityEngine;

public class BookDoorTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] BookLockedStatus _lockedStatus;

    public void Interact()
    {
        _lockedStatus.LockedStatus1();
    }
}
