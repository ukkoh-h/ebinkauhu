using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasKey_1;
    public bool hasKey_2;
    public bool hasKey_3;
    public bool hasKey_4;

    //Oven avaus esim
    /* [SerializeField] bool isLocked; 

    public bool TryOpenDoor(Inventory playerInventory)
    {
        if(isLocked && playerInventory.hasKey_1 == false)
        {
            return false;
        }

        return true;
    }

    */

    public int ammo;
    public int potions;
}
