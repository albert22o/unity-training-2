using UnityEngine;

[CreateAssetMenu(menuName = "inventory/Weapon")]
public class Weapon : Item
{
    public int min_damage;
    public int max_damage;

    public override bool use(PlayerScript player, ItemInstance itemData)
    {
        if (player.activeItem != null)
        {
            player.activeItem.IsEquiped = false;
        }
        player.activeItem = itemData;
        player.activeItem.IsEquiped = true;
        if (player.holder.transform.childCount > 0)
            Destroy(player.holder.transform.GetChild(0).gameObject);

        Instantiate(player.activeItem.itemData.prefab, player.holder.transform);
        return false; // оружие не уничтожается при использовании
    }
}