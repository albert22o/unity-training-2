using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ChestView : MonoBehaviour
{
    [SerializeField] List<Image> icons = new List<Image>();
   
    public void updateUI(Chest chest)
    {
        for (int i = 0; i < chest.Items.Count; i++)
        {
            icons[i].color = new Color(1, 1, 1, 1);

            if (chest.Items[i].IsEquiped)
            {
                icons[i].color = new Color(0, 1, 0, 1); // зеленый для брони
            }

            icons[i].sprite = chest.Items[i].itemData.icon;
        }

        for (int i = chest.Items.Count; i < icons.Count; i++)
        {
            icons[i].color = new Color(1, 1, 1, 0);
            icons[i].sprite = null;
        }
    }
}

