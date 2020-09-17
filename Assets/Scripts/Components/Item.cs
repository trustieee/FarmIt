using UnityEngine;

public class Item : MonoBehaviour
{
    public Texture2D ItemIcon;
    public string ItemName;

    public InventoryItem ToInventoryItem()
    {
        return new InventoryItem { Item = this, Amount = 1 };
    }
}
