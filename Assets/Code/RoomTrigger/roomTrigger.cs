//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class roomTrigger : MonoBehaviour
{
    public Transform Player;
    [SerializeField] GameObject monster1;
    public Despawner piece;
    public monster monster2;
    public bool visibility;
    public bool desapawnMonster;
    public bool respawnMonster;
    public bool spawnBehindWall;

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
        }
    }
}
