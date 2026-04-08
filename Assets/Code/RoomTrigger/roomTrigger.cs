//using System.Threading.Tasks.Dataflow;
using UnityEngine;
using System.Collections;

public class roomTrigger : MonoBehaviour
{
    public Transform Player;
    [SerializeField] GameObject monster1;
    [SerializeField] GameObject respawner;
    [SerializeField] GameObject fallZone;
    public InteractionAudio interactionAudio;
    public Despawner piece;
    public monster monster2;
    public lockedStatus door1;
    public lockedStatus door2;
    public lockedStatus door3;
    public bool visibility;
    public bool desapawnMonster;
    public bool safeRoom;
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
                if (safeRoom){monster2.MusicSafeRoom();}
                else {monster2.MusicAmbient();}
                monster1.SetActive(false);
            }
            if (spawnBehindWall)
            {
                monster2.SpawnMonsterBehindWall();
                monster1.SetActive(true);
                StartCoroutine(CrashCorutine());
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
        private IEnumerator CrashCorutine()
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(interactionAudio.InteractionAudioClips[12], transform.TransformPoint(this.transform.position), interactionAudio.InteractionAudioVolume * 2);
    }
}
