
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "inventory/Armor")]
public class Armor : Item
{
    public int defense;
    public override bool use(PlayerScript player, ItemInstance itemData)
    {
        if (player.activeArmor != null)
        {
            player.activeArmor.IsEquiped = false;
        }
        player.activeArmor = itemData;
        player.activeArmor.IsEquiped = true;
        if (player.armorHolder.transform.childCount > 0)
            Destroy(player.armorHolder.transform.GetChild(0).gameObject);

        Instantiate(player.activeArmor.itemData.prefab, player.armorHolder.transform);
        return false; // броня не уничтожается при использовании
    }
}

