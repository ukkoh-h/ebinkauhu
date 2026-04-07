using UnityEngine;

public class roomVisibility : MonoBehaviour
{
    public Transform Player;
    public monster monster;
    public bool playerInside = false;
    private bool monsterInside = false;

    void Update()
    {

        if (playerInside && !monsterInside)
        {
            monster.cantSee = true;
        } 
        else if (playerInside && monsterInside)
        {
            monster.cantSee = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = true;
        }
        if(other.CompareTag("monster"))
        {
            monsterInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = false;
            monster.cantSee = false;
        }
        if(other.CompareTag("monster"))
        {
            monsterInside = false;
        }
    }
}
