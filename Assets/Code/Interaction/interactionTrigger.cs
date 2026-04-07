using UnityEngine;

public class interactionTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] lockedStatus _lockedStatus;

    public bool doorDirection;
    public bool isDoor = true;
    public Transform monster;
    public void Interact()
    {
       if (isDoor)
        {
            if (!doorDirection)
            {
                _lockedStatus.LockedStatus2();
                _lockedStatus.ChangeLocked();
            } else
            {
                _lockedStatus.LockedStatus1();
            }
            } else
        {
            _lockedStatus.LockedStatus1();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isDoor)
        {
            if(other.CompareTag("monster"))
            {
                if (!doorDirection)
                {
                    _lockedStatus.LockedStatus4();
                } else
                {
                    _lockedStatus.LockedStatus3();
                }
            } 
        }
    }
}