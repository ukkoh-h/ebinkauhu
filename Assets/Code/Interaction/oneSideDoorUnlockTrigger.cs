using UnityEngine;

public class oneSideDoorUnlockTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] lockedStatus _lockedStatus;

    public bool doorDirection;
    public Transform monster;
    bool firstTime = true;
    public void Interact()
    {

            if (doorDirection)
            {
                if (firstTime) 
                {
                    _lockedStatus.ChangeLocked();
                    firstTime=!firstTime;
                }
                _lockedStatus.LockedStatus2();
            } else
            {
                _lockedStatus.LockedStatus1();
            }

    }
    private void OnTriggerEnter(Collider other)
    {

            if(other.CompareTag("monster"))
            {
                if (!doorDirection)
                {
                    _lockedStatus.LockedStatus2();

                } else
                {
                    _lockedStatus.LockedStatus1();
                }
            } 

    }
}

