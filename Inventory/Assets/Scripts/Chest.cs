using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public bool IsFull => items.Count >= maxItems;
    public bool IsEmpty => items.Count == 0;

    public List<ItemInstance> Items => items;

    private List<ItemInstance> items = new List<ItemInstance>();
    [SerializeField] int maxItems = 4;
    [SerializeField] ChestView chestView;

    public void AddItem(ItemInstance item)
    {
        if (items.Count < maxItems)
            items.Add(item);
        chestView.updateUI(this);
    }

    public ItemInstance PopItem()
    {
        if (items.Count == 0)
            return null;
        var item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        chestView.updateUI(this);
        return item;
    }
}

