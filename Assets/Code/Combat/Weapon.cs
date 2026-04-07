using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour, IDataPersistence
{
    private InputMaster controls;
    public PlayerStatus playerStatus;
    
    [SerializeField] private float bulletRange;
    [SerializeField] private float fireRate, reloadTime;
    [SerializeField] private bool isAutomatic;
    [SerializeField] public int magazineSize;
    
    public int ammoLeft;

    //Shoot in 4min vid
    [Header("Bullet Variables")]
    public float bulletSpeed; 
    public float bulletDamage;
    
    [Header("Initial Setup")]
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;
    

    private bool isShooting, readyToShoot, reloading;

    public AudioClip[] WeaponAudioClips;
    [Range(0, 1)] public float WeaponAudioVolume = 0.5f;

    private CharacterController _controller;

    public void LoadData(GameData data)
    {
        this.ammoLeft = data.ammoLeft;
    }

    public void SaveData(ref GameData data)
    {
        data.ammoLeft = this.ammoLeft;
    }

    private void Awake()
    {
        ammoLeft = magazineSize;
        readyToShoot = true;
        controls = new InputMaster();

        controls.Player.Shoot.started += ctx => StartShot();
        controls.Player.Shoot.canceled += ctx => EndShot();

        controls.Player.Reload.performed += ctx => Reload();
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if(isShooting && readyToShoot && !reloading && ammoLeft > 0)
        {
            PerformShot();
        } 
           
    }
    private void StartShot()
    {
        isShooting = true;
        if(isShooting && readyToShoot && !reloading && ammoLeft == 0)
        {
            MagEmpty();
        }
    }
    private void EndShot()
    {
        isShooting = false;
    }
    private void PerformShot()
    {
        readyToShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.FromToRotation(transform.forward, bulletSpawnTransform.forward) * transform.rotation, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);
        //transform.forward = Vector3.Lerp( transform.forward, rigidbody.velocity, Time.deltaTime );
        bullet.GetComponent<Bullet>().damage = bulletDamage;

        AudioSource.PlayClipAtPoint(WeaponAudioClips[1], transform.TransformPoint(_controller.center), WeaponAudioVolume);

        ammoLeft--;

        if(ammoLeft >= 0)
        {
            Invoke("ResetShot", fireRate);

            if(!isAutomatic)
            {
                EndShot();
            }
        }
    }
    private void MagEmpty()
    {
        AudioSource.PlayClipAtPoint(WeaponAudioClips[0], transform.TransformPoint(_controller.center), WeaponAudioVolume);

    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    public void Reload()
    {
        if (playerStatus.ammoPool > 0)
        {
            reloading = true;
            AudioSource.PlayClipAtPoint(WeaponAudioClips[2], transform.TransformPoint(_controller.center), WeaponAudioVolume);
            //Invoke("ReloadFinish", reloadTime);
            ReloadFinish();
        }
        else
        {
            MagEmpty();
        }
        
        
    }

    private void ReloadFinish()
    {
        int reloadAmount = magazineSize - ammoLeft;
        reloadAmount = (playerStatus.ammoPool - reloadAmount) >= 0 ? reloadAmount : playerStatus.ammoPool;
        ammoLeft += reloadAmount;
        playerStatus.ammoPool -= reloadAmount;

        reloading = false;
    }

    public void AddAmmo(int ammoAmount)
    {
        playerStatus.ammoPool += ammoAmount;
        if(playerStatus.ammoPool > playerStatus.maxPlayerAmmo)
        {
            playerStatus.ammoPool = playerStatus.maxPlayerAmmo;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
