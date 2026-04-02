using UnityEngine;

public class HealthPickup : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public int healthPickupAmount = 3;
    private bool collected = false;

    public void LoadData(GameData data)
    {
        data.potionsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.potionsCollected.ContainsKey(id))
        {
            data.potionsCollected.Remove(id);
        }
        data.potionsCollected.Add(id, collected);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(!collected)
        {
            PlayerStatus playerStatus = collision.gameObject.GetComponentInChildren<PlayerStatus>();
            if (playerStatus)
            {
                playerStatus.AddPotions(healthPickupAmount);
                collected = true;
                Destroy(gameObject);
            }
        }
    }
}