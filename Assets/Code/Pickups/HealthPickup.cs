using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthPickupAmount = 3;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        PlayerStatus playerStatus = collision.gameObject.GetComponentInChildren<PlayerStatus>();
        if (playerStatus)
        {
            playerStatus.AddPotions(healthPickupAmount);
            Destroy(gameObject);
        }
    }
}