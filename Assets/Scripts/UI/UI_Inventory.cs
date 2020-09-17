using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour, IPointerClickHandler
{
    public event EventHandler<Item> InventoryItemLeftClicked;
    public event EventHandler<Item> InventoryItemMiddleClicked;

    private Inventory _inventory;
    private List<RawImage> _inventorySlots;

    private void Start()
    {
        _inventory = GetComponentInParent<Inventory>();
        _inventorySlots = new List<RawImage>(GetComponentsInChildren<RawImage>());
    }

    private void Update()
    {
        for (int i = 0; i < _inventory.Items.Count(); i++)
        {
            var actualInventoryItem = _inventory.Items.ElementAt(i);
            _inventorySlots[i].texture = actualInventoryItem.Item.ItemIcon;
            _inventorySlots[i].GetComponentInChildren<TMP_Text>().SetText(actualInventoryItem.Amount.ToString());
            _inventorySlots[i].GetComponent<UI_InventorySlot>().Item = actualInventoryItem.Item;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        var uiInventorySlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UI_InventorySlot>();
        if (uiInventorySlot != null && !string.IsNullOrWhiteSpace(uiInventorySlot.Item?.ItemName))
        {
            Debug.Log($"Clicked: {uiInventorySlot.Item.ItemName}");
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    InventoryItemLeftClicked?.Invoke(this, uiInventorySlot.Item);
                    break;
                case PointerEventData.InputButton.Middle:
                    InventoryItemMiddleClicked?.Invoke(this, uiInventorySlot.Item);
                    break;
            }
        }
    }
}
