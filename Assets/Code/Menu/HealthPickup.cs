using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //[SerializeField] public int ammoPickupAmount;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        PlayerStatus playerStatus = collision.gameObject.GetComponentInChildren<PlayerStatus>();
        if (playerStatus)
        {
            playerStatus.AddPotions(playerStatus.healthPickupAmount);
            Destroy(gameObject);
        }
    }
}