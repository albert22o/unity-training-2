using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public bool IsFull => items.Count >= maxItems;
    public bool IsEmpty => items.Count == 0;    
    private List<ItemInstance> items = new List<ItemInstance>();
    [SerializeField] int maxItems = 4;
    [SerializeField] List<Image> icons = new List<Image>();

    public void updateUI()
    {
        for (int i = 0; i < items.Count; i++)
        {
            icons[i].color = new Color(1, 1, 1, 1);

            if (items[i].IsEquiped)
            {
                icons[i].color = new Color(0, 1, 0, 1); // зеленый для брони
            }

            icons[i].sprite = items[i].itemData.icon;
        }

        for (int i = items.Count; i < icons.Count; i++)
        {
            icons[i].color = new Color(1, 1, 1, 0);
            icons[i].sprite = null;
        }
    }
    public void AddItem(ItemInstance item)
    {
        if (items.Count < maxItems)
            items.Add(item);
    }

    public ItemInstance PopItem()
    {
        if (items.Count == 0)
            return null;
        var item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }
}

