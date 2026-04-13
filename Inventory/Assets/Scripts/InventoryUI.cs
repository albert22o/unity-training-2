using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public ItemMenu menu;
    public Inventory inventory;
    [SerializeField] List<Image> icons = new List<Image>();
    [SerializeField] List<TMP_Text> amounts = new List<TMP_Text>();

    public void updateUI()
    {
        for (int i = 0; i < inventory.getSize(); i++)
        {
            icons[i].color = new Color(1, 1, 1, 1);
            icons[i].sprite = inventory.getItem(i).itemData.icon;
            amounts[i].text = (inventory.getAmount(i) > 1)
                ? inventory.getAmount(i).ToString() : "";
        }

        for (int i = inventory.getSize(); i < icons.Count; i++)
        {
            icons[i].color = new Color(1, 1, 1, 0);
            icons[i].sprite = null;
            amounts[i].text = "";
        }
    }

    public void showMenu(int i)
    {
        if (inventory.getItem(i) != null)
            menu.show(icons[i].transform, i);
    }
}