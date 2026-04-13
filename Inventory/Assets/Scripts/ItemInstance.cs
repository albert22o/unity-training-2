using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    [SerializeReference] public Item itemData;
    [SerializeField] public int damage;
    public bool IsEquiped = false;

    public bool use(PlayerScript player)
    {
        return itemData.use(player, this);
    }
}