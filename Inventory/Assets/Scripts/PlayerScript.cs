using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10;
    public float angular_speed = 180;
    public float interaction_range = 2;
    public LayerMask items;

    CharacterController cc;
    public TMP_Text description;

    public Transform holder;         // пустой объект "рука" в иерархии персонажа
    public Transform armorHolder;    // пустой объект "броня" в иерархии персонажа
    public ItemInstance activeItem;  // экипированное оружие
    public ItemInstance activeArmor; // экипированная броня

    Animator anim;
    public Stats stats = new Stats();

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        activeItem = null;
        activeArmor = null;
    }

    void LateUpdate()
    {
        float yRotation = Input.GetAxisRaw("Horizontal");
        float forwardMove = Input.GetAxisRaw("Vertical");

        transform.Rotate(new Vector3(0, yRotation * angular_speed * Time.deltaTime, 0));

        Vector3 dir = new Vector3(0, 0, forwardMove);
        dir.Normalize();
        dir = transform.TransformDirection(dir);
        cc.Move(dir * speed * Time.deltaTime);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, interaction_range, items))
        {
            // не показывать имя предмета, взятого в руку
            if (hit.transform.GetComponent<ItemContainer>().transform.parent != holder)
                description.text = hit.transform.GetComponent<ItemContainer>().item.itemData.item_name;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ItemInstance item = hit.transform.GetComponent<ItemContainer>().item;
                int amount = hit.transform.GetComponent<ItemContainer>().amount;
                int remaining = GetComponent<Inventory>().addItems(item, amount);
                hit.transform.GetComponent<ItemContainer>().pickUp(remaining);
            }
        }
        else
        {
            description.text = "";
        }

        // атака экипированным оружием
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (activeItem != null)
                anim.SetTrigger(activeItem.itemData.action);
        }
    }

    public void use(int i)
    {
        ItemInstance item = GetComponent<Inventory>().getItem(i);
        if (item == null) return;

        if (item.use(this))
            GetComponent<Inventory>().removeItem(i);
    }

    public void drop(int i)
    {
        ItemInstance item = GetComponent<Inventory>().getItem(i);
        if (item == null) return;

        if (activeItem == item)
        {
            Destroy(holder.transform.GetChild(0).gameObject);
            activeItem = null;
        }
        if (activeArmor == item)
        {
            Destroy(armorHolder.transform.GetChild(0).gameObject);
            activeArmor = null;
        }
        GetComponent<Inventory>().dropItem(i);
    }

    public void destroy(int i)
    {
        ItemInstance item = GetComponent<Inventory>().getItem(i);
        if (item == null) return;

        if (activeItem == item)
        {
            Destroy(holder.transform.GetChild(0).gameObject);
            activeItem = null;
        }
        if (activeArmor == item)
        {
            Destroy(armorHolder.transform.GetChild(0).gameObject);
            activeArmor = null;
        }
        GetComponent<Inventory>().destroyItem(i);
    }
}