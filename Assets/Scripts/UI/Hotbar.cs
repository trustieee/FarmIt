using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private UI_Inventory _uiInventory;
    private List<RawImage> _hotbarSlots;

    private void Start()
    {
        _hotbarSlots = new List<RawImage>(GetComponentsInChildren<RawImage>());
        _uiInventory = transform.parent.GetComponentInChildren<UI_Inventory>();

        _uiInventory.InventoryItemMiddleClicked += HandleInventoryItemMiddleClicked;
    }

    private void HandleInventoryItemMiddleClicked(object sender, Item item)
    {
        var firstAvailableHotbarSlot = _hotbarSlots.FirstOrDefault(h => h.GetComponent<UI_HotbarSlot>().Item == null) ?? _hotbarSlots.First();
        firstAvailableHotbarSlot.texture = item.ItemIcon;
        firstAvailableHotbarSlot.GetComponent<UI_HotbarSlot>().Item = item;
    }

    public Item GetItemAtIndex(int index) => _hotbarSlots[index].GetComponent<UI_HotbarSlot>().Item;
}
