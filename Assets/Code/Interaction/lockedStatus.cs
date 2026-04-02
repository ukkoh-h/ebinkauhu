using UnityEngine;

public class lockedStatus : MonoBehaviour
{
    [SerializeField] door2 _door1;
    [SerializeField] door2 _door2;
    [SerializeField] lockedInteractable _lockedInt;
    public bool locked1 = false;
    public bool locked2 = false;
    public bool locked3 = false;

    public void LockedStatus1()
    {
        if (!locked1 && !locked2 && !locked3)
        {
            _door1?.Open1();
            _door2?.Open2();
            _lockedInt?.Interact();
        } else
        {
            Debug.Log("IT'S LOCKED!");
        }
    }    
    public void LockedStatus2()
    {
        if (!locked1 && !locked2 && !locked3)
        {
            _door1?.Open2();
            _door2?.Open1();
        } else
        {
            Debug.Log("IT'S LOCKED!");
        }
    }
    public void ChangeLocked()
    {
        if (locked1)
        {
            locked1 = false;
        } else if (locked2)
        {
            locked2 = false;
        } else if (locked3)
        {
            locked3 = false;
            Debug.Log("IT'S UNLOCKED!");
        }
    }
}
