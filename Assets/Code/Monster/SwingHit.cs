using UnityEngine;

public class SwingHit : MonoBehaviour
{
    //public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerStatus>() != null)
        {
            if(other.GetComponent<PlayerStatus>().playerHealth == 1)
            {
                other.GetComponent<PlayerStatus>().OnDeath();
            }
            else if(other.GetComponent<PlayerStatus>().playerHealth == 0)
            {
                return;
            } 
            else
            {
                other.GetComponent<PlayerStatus>().OnHit();
            }
            
            other.GetComponent<PlayerStatus>().playerHealth -= 1;
            
            
        }
        
        
        
    }
}
