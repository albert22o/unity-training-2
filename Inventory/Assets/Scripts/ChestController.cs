using UnityEngine;

class ChestController : MonoBehaviour
{
    [SerializeField] Chest chest;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject chestUI;

    public void OnTriggerEnter(Collider other)
    {
        chestUI.SetActive(true);
        chest.updateUI();
    }

    public void OnTriggerExit(Collider other)
    {
        chestUI.SetActive(false);
        chest.updateUI();
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
        chest.updateUI();
    }

    public void RemoveItem()
    {
        if (chest.IsEmpty)
            return;
        var item = chest.PopItem();
        inventory.addItems(item, 1);
        chest.updateUI();
    }
}
