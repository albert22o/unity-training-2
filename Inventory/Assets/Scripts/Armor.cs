
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "inventory/Armor")]
public class Armor : Item
{
    public int defense;
    public override bool use(PlayerScript player, ItemInstance itemData)
    {

        UnEquipe(player);
        player.activeArmor = itemData;
        player.activeArmor.IsEquiped = true;


        Instantiate(player.activeArmor.itemData.prefab, player.armorHolder.transform);
        return false; // броня не уничтожается при использовании
    }

    public void UnEquipe(PlayerScript player)
    {
        if (player.activeArmor != null)
        {
            player.activeArmor.IsEquiped = false;
            if (player.armorHolder.transform.childCount > 0)
                Destroy(player.armorHolder.transform.GetChild(0).gameObject);
        }
    }
}

