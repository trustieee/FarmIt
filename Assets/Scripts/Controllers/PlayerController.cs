using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FirstPersonAIO _firstPersonController;
    private Vitals _vitals;
    private Inventory _inventory;
    private Interact _interact;
    private Place _place;

    private void Start()
    {
        _firstPersonController = GetComponent<FirstPersonAIO>();
        _vitals = GetComponent<Vitals>();
        _inventory = GetComponent<Inventory>();
        _interact = GetComponent<Interact>();
        _place = GetComponent<Place>();

        _vitals.Died += HandleDied;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            var loot = _interact.PerformInteraction();
            _inventory.AddItems(loot);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            _place.PerformPlace();
        }
        else if (Input.GetButtonDown("Inventory"))
        {
            ToggleCursor();
            _inventory.ToggleInventory();
        }
    }

    private void HandleDied(object sender, EventArgs e)
    {
        _inventory.enabled = false;
        _firstPersonController.enabled = false;
    }

    private void ToggleCursor()
    {
        // this asset is kinda lacking in this regard... this method actually freezes all momentum as well.
        _firstPersonController.ControllerPause();
    }
}
