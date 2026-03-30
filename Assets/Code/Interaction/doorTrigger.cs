using UnityEngine;

public class doorTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] lockedStatus _lockedStatus;
    public bool doorDirection;
    public Transform monster;
    public void Interact()
    {
        if (!doorDirection)
        {
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
