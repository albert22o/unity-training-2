using System;
using UnityEngine;

[CreateAssetMenu(menuName = "inventory/Weapon")]
public class Weapon : Item
{
    public int min_damage;
    public int max_damage;

    public override bool use(PlayerScript player, ItemInstance itemData)
    {
        UnEquipe(player);

        player.activeItem = itemData;
        player.activeItem.IsEquiped = true;


        Instantiate(player.activeItem.itemData.prefab, player.holder.transform);
        return false; // оружие не уничтожается при использовании
    }

    public void UnEquipe(PlayerScript playerScript)
    {
        if (playerScript.activeItem != null)
        {
            playerScript.activeItem.IsEquiped = false;
            if (playerScript.holder.transform.childCount > 0)
                Destroy(playerScript.holder.transform.GetChild(0).gameObject);
        }
    }
}