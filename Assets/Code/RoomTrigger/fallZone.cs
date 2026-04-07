using UnityEngine;

public class fallZone : MonoBehaviour
{
    //private bool monsterInside = false;
    public lockedStatus lockStat;
    public lever lever;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("monster"))
        {
            //monsterInside = true;
            lockStat.MonsterChangeLocked();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("monster"))
        {
            //monsterInside = false;
            lockStat.MonsterChangeLocked();
        }
    }
}
