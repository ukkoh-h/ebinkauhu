using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int ammoPool;

    public Vector3 spawnPoint = new Vector3(-3, 0, 7);

    public int ammoLeft;
    public int healthItemPool;
    public int playerHealth;

    public Vector3 playerPosition;
    //public Dictionary<string, bool> itemCollected
    public SerializableDictionary<string, bool> ammoCollected;
    public SerializableDictionary<string, bool> potionsCollected;
    public SerializableDictionary<string, bool> keysCollected;
    public SerializableDictionary<string, bool> lockedState;
    public SerializableDictionary<string, bool> itemActiveState;

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
        this.ammoPool = 120;
        this.ammoLeft = 5;
        this.healthItemPool = 45;
        this.playerHealth = 6;
        
        //Laitetaan halutut aloitus koordinaatit tilalle sit
        playerPosition = spawnPoint;
        ammoCollected = new SerializableDictionary<string, bool>();
        potionsCollected = new SerializableDictionary<string, bool>();
        keysCollected = new SerializableDictionary<string, bool>();
        lockedState = new SerializableDictionary<string, bool>();
        itemActiveState = new SerializableDictionary<string, bool>();

        this.hasWeapon_2 = false;

        this.hasKey_1 = false;
        this.hasKey_2 = false;
        this.hasKey_3 = false;
        this.hasKey_4 = false;
    }
}
