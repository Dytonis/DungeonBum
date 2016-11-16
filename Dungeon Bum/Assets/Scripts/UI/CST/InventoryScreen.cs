using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.Entity.Items;

public class InventoryScreen : MonoBehaviour
{
    public Inventory Inventory;
    public InventorySlot[] Slots;
    public InventorySlot DraggingOver;
    public bool Dragging;
    public InventorySlot CurrentlyDragging;
    public ItemHoverPopup Popup;

	// Use this for initialization
	void Start () {
	
	}
	
    public void Reset()
    {
        foreach(InventorySlot s in Slots)
        {
            s.ItemImage.transform.position = s.firstPostion;
            s.CheckForDrag = false;
        }

        DraggingOver = null;
        Dragging = false;
        if(Popup != null)
        {
            Destroy(Popup.gameObject);
            Popup = null;
        }
        CurrentlyDragging = null;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Popup != null)
        {
            if (Input.mousePosition.x - (126.7f / 2) <= 0)
            {
                Popup.transform.position = new Vector3((126.7f / 2), Input.mousePosition.y);
            }
            else
            {
                Popup.transform.position = Input.mousePosition;
            }
        }
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
