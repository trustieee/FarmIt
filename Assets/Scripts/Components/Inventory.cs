using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public IEnumerable<InventoryItem> Items => _items.Values.AsEnumerable();

    private Dictionary<string, InventoryItem> _items = new Dictionary<string, InventoryItem>();
    private bool _showInventory;

    private UI_Inventory _uiInventory;

    private void Start()
    {
        _uiInventory = GetComponentInChildren<UI_Inventory>();
        if (_uiInventory != null)
        {
            _uiInventory.gameObject.SetActive(_showInventory);
        }
    }

    public void ToggleInventory(bool? show = null)
    {
        if (_uiInventory == null)
            return;

        _showInventory = show ?? !_showInventory;

        Debug.Log($"Show Inventory: {_showInventory}");
        _uiInventory.gameObject.SetActive(_showInventory);
    }

    public void AddItems(List<Item> items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    public void AddItem(Item item)
    {
        Debug.Log($"Looted {item.ItemName}");
        if (_items.ContainsKey(item.ItemName))
        {
            // todo: implement max item amount?
            _items[item.ItemName].Amount++;
        }
        else
        {
            _items.Add(item.ItemName, item.ToInventoryItem());
        }
    }

    public InventoryItem GetItem(string itemName) => _items[itemName];
}
