//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class roomTrigger : MonoBehaviour
{
    public Transform Player;
    [SerializeField] GameObject monster1;
    [SerializeField] GameObject respawner;
    [SerializeField] GameObject fallZone;
    public Despawner piece;
    public monster monster2;
    public lockedStatus door1;
    public lockedStatus door2;
    public lockedStatus door3;
    public bool visibility;
    public bool desapawnMonster;
    public bool respawnMonster;
    public bool spawnBehindWall;
    public bool finalScene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!visibility) monster2.cantSee = true;
            if (desapawnMonster) 
            {
                monster1.SetActive(false);
            }
            if (spawnBehindWall)
            {
                monster2.SpawnMonsterBehindWall();
                monster1.SetActive(true);
                piece.Despawn();
                spawnBehindWall = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!visibility) monster2.cantSee = false;
            if (respawnMonster)
            {
                monster2.RespawnMonster();
                monster1.SetActive(true);
            }
            if (finalScene)
            {
                monster2.PlaceForFinalFight();
                monster1.SetActive(true);
                respawner.SetActive(false);
                fallZone.SetActive(true);
                door1.locked1 = true;
                door2.locked1 = true;
                door3.locked1 = true;
            }
        }
    }
}
