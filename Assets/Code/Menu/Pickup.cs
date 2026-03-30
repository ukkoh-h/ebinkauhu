using UnityEngine;

public class Pickup : MonoBehaviour
{
    //[SerializeField] public int ammoPickupAmount;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        Weapon weapon = collision.gameObject.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.AddAmmo(weapon.pickupAmount);
            Destroy(gameObject);
        }
    }
}
