using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.Entity.Items;

public class InventoryScreen : MonoBehaviour
{
    public Inventory Inventory;
    public InventorySlot[] Slots;
    public InventorySlot DraggingOver;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInventoryVisually(Inventory inv)
    {
        foreach (InventorySlot s in Slots)
        {
            s.ItemImage.color = new Color(1, 1, 1, 0);
            s.ItemRepresenting = null;
        }

        foreach (Item i in inv.CurrentInventory)
        {
            Slots[i.InventoryPosition].ItemRepresenting = i;
            Slots[i.InventoryPosition].ItemImage.sprite = i.GetComponent<SpriteRenderer>().sprite;
            Slots[i.InventoryPosition].ItemImage.color = new Color(1, 1, 1, 1);
        }
    }
}
