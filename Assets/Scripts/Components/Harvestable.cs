using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public Item Loot;
    public bool DestroyOnHarvest = false;
    public int MinLootable;
    public int MaxLootable;

    public List<Item> Harvest()
    {
        var take = new List<Item>();
        for (int i = 0; i < Random.Range(MinLootable, MaxLootable + 1); i++)
        {
            take.Add(Loot);
        }

        if (DestroyOnHarvest)
        {
            Destroy(gameObject, 1f);
        }

        return take;
    }
}
