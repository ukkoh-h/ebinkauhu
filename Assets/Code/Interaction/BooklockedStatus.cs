using UnityEngine;

public class BookLockedStatus : MonoBehaviour
{
    [SerializeField] doorBookcase _door1;
    public bool locked = true;
    public void LockedStatus1()
    {
        if (!locked)
        {
            _door1.Open1();
        } else
        {
            Debug.Log("DOOR'S LOCKED!");
        }
    }    
        public void ChangeLocked()
    {
        locked = !locked;
    }

}
