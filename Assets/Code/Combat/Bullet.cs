using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float lifeTime = 4;


    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime < 0)
            Destroy(gameObject);
        /* if (rigidbody.velocity.x > 0.3 || rigidbody.velocity.y > 0.3 || rigidbody.velocity.z > 0.3)
        transform.LookAt(transform.position + rigidbody.velocity); */
        //transform.forward = Vector3.Lerp( transform.forward, rigidbody.velocity, Time.deltaTime );
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Target>() != null)
            other.GetComponent<Target>().health -= damage;
        else if(other.GetComponent<monster>() != null)
            other.GetComponent<monster>().health -= damage;
        Destroy(gameObject);
    }
}