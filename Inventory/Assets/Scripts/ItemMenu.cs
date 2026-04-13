using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    public PlayerScript player;
    public InventoryUI inventoryUI;
    public int i;

    public void use()
    {
        player.use(i);
        inventoryUI.updateUI();
        hide();
    }

    public void drop()
    {
        player.drop(i);
        hide();
    }

    public void destroy()
    {
        player.destroy(i);
        hide();
    }

    public void show(Transform parent, int ind)
    {
        i = ind;
        transform.SetParent(parent, false);
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
            hide();
    }
}