using System;
using UnityEngine;

public class ChestGenericController : MonoBehaviour
{
    private Chest chest;
    [SerializeField] Inventory inventory;

    public void SetChest(Chest chest)
    {
        this.chest = chest;
    }

    public void AddItem()
    {
        if (chest.IsFull)
            return;
        if (inventory.slots.Count == 0)
            return;
        var item = inventory.getItem(inventory.slots.Count - 1);
        inventory.removeItem(inventory.slots.Count - 1);
        chest.AddItem(item);
    }

    public void RemoveItem()
    {
        if (chest.IsEmpty)
            return;
        var item = chest.PopItem();
        inventory.addItems(item, 1);
    }
}