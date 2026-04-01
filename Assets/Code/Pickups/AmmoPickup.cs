using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoPickupAmount = 15;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        Weapon weapon = collision.gameObject.GetComponentInChildren<Weapon>();
        if (weapon)
        {
            weapon.AddAmmo(ammoPickupAmount);
            Destroy(gameObject);
        }
    }
}