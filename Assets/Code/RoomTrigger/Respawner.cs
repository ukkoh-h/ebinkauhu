using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform Player;
    [SerializeField] GameObject monster1;
    public monster monster2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!monster1.activeSelf)
            {
                monster2.RespawnMonster();
                monster1.SetActive(true);
            }
        }
    }
}
