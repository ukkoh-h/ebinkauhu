using UnityEngine;

public class SwingHit : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Target>() != null) other.GetComponent<Target>().health -= damage;
    }
}
