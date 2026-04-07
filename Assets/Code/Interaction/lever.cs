using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class lever : MonoBehaviour
{
    [SerializeField] GameObject chandelier1;
    [SerializeField] GameObject chandelier2;
    [SerializeField] GameObject chandelier3;
    [SerializeField] GameObject monster1;
    [SerializeField] GameObject monster2;
    [SerializeField] GameObject finsher;
    public CinemachineCamera activeCam;
    public void MonsterSmash()
    {
        chandelier1.SetActive(false);
        chandelier2.SetActive(true);
        activeCam.Priority = 2;
        chandelier3.SetActive(true);
        monster1.SetActive(false);
        monster2.SetActive(true);
        StartCoroutine(CamFocusCorutine());
        finsher.SetActive(true);
    }

        private IEnumerator CamFocusCorutine()
    {
        yield return new WaitForSeconds(1f);
        chandelier2.SetActive(false);
        activeCam.Priority = 0;
    }
}
