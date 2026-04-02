using UnityEngine;

public class oneSideDoorUnlockTrigger : MonoBehaviour
{
    [SerializeField] lockedStatus _lockedStatus;

    public bool doorDirection;
    public Transform monster;
    public void Interact()
    {

            if (!doorDirection)
            {
                _lockedStatus.LockedStatus2();
                _lockedStatus.ChangeLocked();
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
                    _lockedStatus.MonsterChangeLocked();
                    _lockedStatus.LockedStatus2();

                } else
                {
                    _lockedStatus.MonsterChangeLocked();
                    _lockedStatus.LockedStatus1();
                }
            } 

    }
        private void OnTriggerExit(Collider other)
    {

            if(other.CompareTag("monster"))
            {
                _lockedStatus.MonsterChangeLocked();
            } 

    }
}

