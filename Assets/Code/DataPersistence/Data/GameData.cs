using UnityEngine;

[System.Serializable]

public class GameData
{
    public int deathCount;
    public int ammoPool;
    public int healthItemPool;
    public int playerHealth;

    public bool hasWeapon_2;

    public bool hasKey_1;
    public bool hasKey_2;
    public bool hasKey_3;
    public bool hasKey_4;

    /* ammoPool
    healthItemPool
    playerHealth

    hasWeapon_2

    hasKey_1
    hasKey_2
    hasKey_3
    hasKey_4 */

    public GameData()
    {
        this.ammoPool = 35;
        this.healthItemPool = 5;
        this.playerHealth = 6;

        this.hasWeapon_2 = false;

        this.hasKey_1 = false;
        this.hasKey_2 = false;
        this.hasKey_3 = false;
        this.hasKey_4 = false;
    }
}
