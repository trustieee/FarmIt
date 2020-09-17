using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FirstPersonAIO _firstPersonController;
    private Vitals _vitals;
    private Inventory _inventory;
    private Interact _interact;
    private Place _place;
    private HeldItem _heldItem;
    private UI_Inventory _uiInventory;
    private Hotbar _hotbar;

    private void Start()
    {
        _firstPersonController = GetComponent<FirstPersonAIO>();
        _vitals = GetComponent<Vitals>();
        _inventory = GetComponent<Inventory>();
        _interact = GetComponent<Interact>();
        _place = GetComponent<Place>();
        _heldItem = GetComponentInChildren<HeldItem>();
        _uiInventory = GetComponentInChildren<UI_Inventory>(true);
        _hotbar = GetComponentInChildren<Hotbar>();

        _vitals.Died += HandleDied;
        _uiInventory.InventoryItemLeftClicked += HandleInventoryItemClicked;
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
            // todo: figure out better way to handle eat versus use place
            if (_heldItem != null && _heldItem.Item != null)
            {
                var edible = _heldItem.Item.GetComponent<Edible>();
                if (edible != null)
                {
                    _vitals.RestoreHunger(edible.HungerAmount);
                }
            }
            _place.PerformPlace();
        }
        else if (Input.GetButtonDown("Inventory"))
        {
            ToggleCursor();
            _inventory.ToggleInventory();
        }

        // todo: handle hotbar keybinds better
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetButtonDown($"Hotbar{i}"))
            {
                EquipItem(_hotbar.GetItemAtIndex(i - 1));
            }
        }
    }

    private void HandleDied(object sender, EventArgs e)
    {
        _inventory.enabled = false;
        _firstPersonController.enabled = false;
    }

    private void ToggleCursor()
    {
        // todo: this asset is kinda lacking in this regard... this method actually freezes all momentum as well.
        _firstPersonController.ControllerPause();
    }

    private void HandleInventoryItemClicked(object sender, Item item) => EquipItem(item);

    private void EquipItem(Item item)
    {
        if (item == null || item.ItemName == _heldItem.Item?.ItemName)
            return;

        // todo: remove hardcode plantable
        Destroy(_heldItem.Item);
        _heldItem.Item = Instantiate(Resources.Load<GameObject>($"Plantables/{item.ItemName}"),
                                    _heldItem.transform.position,
                                    Quaternion.identity,
                                    _heldItem.transform).GetComponent<Item>();
    }
}
