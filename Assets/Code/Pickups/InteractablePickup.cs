using UnityEngine;

public class InteractablePickup : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _health;
    [SerializeField] GameObject _ammo;

    public int ammoPickupAmount = 5;
    public int healthPickupAmount = 3;
    public bool healthPick;
    public bool ammoPick;
    void Start()
    {
        if (healthPick) _health?.SetActive(true);
        if (ammoPick) _ammo?.SetActive(true);
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
    public void Pickup()
    {
        if (ammoPick)
        {
            Weapon weapon = _player.GetComponentInChildren<Weapon>();
            if (weapon)
            {
                weapon.AddAmmo(ammoPickupAmount);
            }
        }
        if (healthPick)
        {
            PlayerStatus playerStatus = _player.GetComponentInChildren<PlayerStatus>();
            if (playerStatus)
            {
                playerStatus.AddPotions(healthPickupAmount);
            }
        }
    }
}
