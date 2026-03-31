using UnityEngine;

public class lockedStatus : MonoBehaviour
{
    [SerializeField] door2 _door;
    public bool locked = false;
    public void LockedStatus1()
    {
        if (!locked)
        {
            _door.Open1();
        } else
        {
            Debug.Log("DOOR'S LOCKED!");
        }
    }    
    public void LockedStatus2()
    {
        if (!locked)
        {
            _door.Open2();
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
