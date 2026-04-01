using UnityEngine;

public class KeyPickup : MonoBehaviour
//, IDataPersistence
{
    /* [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public int ammoPickupAmount = 15;
    private bool collected = false;

    public void LoadData(GameData data)
    {
        
        data.ammoCollected.TryGetValue(id, out collected);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.ammoCollected.ContainsKey(id))
        {
            data.ammoCollected.Remove(id);
        }
        data.ammoCollected.Add(id, collected);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
}

    private void OnTriggerEnter(Collider collision)
    {
        if(!collected)
        {
            Weapon weapon = collision.gameObject.GetComponentInChildren<Weapon>();
            if (weapon)
            {
                weapon.AddAmmo(ammoPickupAmount);
                collected = true;
                Destroy(gameObject);
            }
        }
    } */
}