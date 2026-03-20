using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /* private InputMaster controls;
    public GameObject InnventoryMenu;

    private int ammoLeft;

    private bool menuActivated, readyToOpenMenu;

    private void Awake()
    {
        ammoLeft = 1;
        readyToOpenMenu = true;
        controls = new InputMaster();

        controls.Player.Inventory.started += ctx => StartMenu();
        controls.Player.Inventory.canceled += ctx => EndMenu();
    }

    private void Update()
    {
        if (menuActivated && readyToOpenMenu && ammoLeft > 0) PerformMenu();
        if (!menuActivated && ammoLeft == 0) EndMenu();
           
    }

    private void StartMenu()
    {
        menuActivated = true;
    }

    private void EndMenu()
    {
        menuActivated = false;
        ammoLeft = 1;
    }

    private void PerformMenu()
    {
        readyToOpenMenu = false;
        InnventoryMenu.SetActive(true);

        ammoLeft--;
        if(ammoLeft >= 0)
        {
            Invoke("ResetMenu", 1);
            EndMenu();

        }
        
    }

    private void ResetMenu()
    {
        ammoLeft = 1;
        readyToOpenMenu = true;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    } */
}
