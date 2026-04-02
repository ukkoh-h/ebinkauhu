using UnityEngine;

public class SwingHit : MonoBehaviour
{
    //public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerStatus>() != null) other.GetComponent<PlayerStatus>().playerHealth -= 1;
    }
}
