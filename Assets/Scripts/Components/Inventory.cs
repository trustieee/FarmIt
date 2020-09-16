using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public IEnumerable<Item> Items => _items.AsEnumerable();

    private List<Item> _items = new List<Item>();
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
        _items.Add(item);
    }
}
