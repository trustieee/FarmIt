using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    private List<RawImage> _inventorySlots;

    private void Start()
    {
        _inventory = GetComponentInParent<Inventory>();
        // todo: don't hardcode me bro
        _inventorySlots = new List<RawImage>(20);
    }

    private void Update()
    {
        for (int i = 0; i < _inventory.Items.Count(); i++)
        {
            _inventorySlots[i].texture = _inventory.Items.ElementAt(i).ItemIcon;
        }
    }
}
