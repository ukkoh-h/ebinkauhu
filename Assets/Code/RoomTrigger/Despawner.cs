using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour
{
    [SerializeField] GameObject pice1;
    [SerializeField] GameObject pice2;
    [SerializeField] GameObject pice3;
    [SerializeField] GameObject pice4;
    [SerializeField] GameObject pice5;
    [SerializeField] GameObject wall;
    public void Despawn()
    {
        wall.SetActive(false);
        StartCoroutine(BlockDespawnerCorutine());
    }
    private IEnumerator BlockDespawnerCorutine()
    {
        yield return new WaitForSeconds(1.5f);
        pice1.SetActive(false);
        pice2.SetActive(false);
        pice3.SetActive(false);
        pice4.SetActive(false);
        pice5.SetActive(false);
    }
}
