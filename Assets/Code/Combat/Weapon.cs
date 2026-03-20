using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private InputMaster controls;

    //private RaycastHit rayHit;

    
    [SerializeField] private float bulletRange;
    [SerializeField] private float fireRate, reloadTime;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private int magazineSize;
    private int ammoLeft;

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
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().damage = bulletDamage;
        /* Vector3 direction = transform.forward;

        if(Physics.Raycast(transform.position, direction, out rayHit, bulletRange))
        {
            Debug.Log(rayHit.collider.gameObject.name);
        } */

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
    private void Reload()
    {
        reloading = true;
        AudioSource.PlayClipAtPoint(WeaponAudioClips[2], transform.TransformPoint(_controller.center), WeaponAudioVolume);
        Invoke("ReloadFinish", reloadTime);
    }

    private void ReloadFinish()
    {
        ammoLeft = magazineSize;
        reloading = false;
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
