using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public List<Item> PerformInteraction()
    {
        var loot = new List<Item>();

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit, 3f))
        {
            var harvestable = hit.collider.gameObject.transform.root.GetComponentInChildren<Harvestable>();
            if (harvestable != null)
            {
                loot = harvestable.Harvest();
            }
        }

        return loot;
    }
}
