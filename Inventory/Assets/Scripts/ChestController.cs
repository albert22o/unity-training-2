using UnityEngine;

class ChestController : MonoBehaviour
{
    [SerializeField] Chest chest;
    [SerializeField] GameObject chestUI;
    [SerializeField] ChestGenericController chestGenericController;
    [SerializeField] ChestView chestView;

    public void OnTriggerEnter(Collider other)
    {
        chestUI.SetActive(true);
        chestGenericController.SetChest(chest);
        chestView.updateUI(chest);
    }

    public void OnTriggerExit(Collider other)
    {
        chestUI.SetActive(false);
    }


}
