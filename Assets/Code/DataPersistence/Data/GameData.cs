using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int deathCount;
    public int ammoPool;
    public int healthItemPool;
    public int playerHealth;

    public Vector3 playerPosition;
    //public Dictionary<string, bool> itemCollected
    public Dictionary<string, bool> ammoCollected;
    public Dictionary<string, bool> potionsCollected;
    public Dictionary<string, bool> keysCollected;

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
        
        //Laitetaan halutut aloitus koordinaatit tilalle sit
        playerPosition = Vector3.zero;
        ammoCollected = new Dictionary<string, bool>();
        potionsCollected = new Dictionary<string, bool>();
        keysCollected = new Dictionary<string, bool>();

        this.hasWeapon_2 = false;

        this.hasKey_1 = false;
        this.hasKey_2 = false;
        this.hasKey_3 = false;
        this.hasKey_4 = false;
    }
}
