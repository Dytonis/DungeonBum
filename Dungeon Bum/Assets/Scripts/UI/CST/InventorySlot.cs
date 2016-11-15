using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts.Entity.Items;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Item ItemRepresenting;
    public InventoryScreen Screen;
    public Image ItemImage;
    public bool Dragging;
    public int id;
    private Vector3 firstPostion;
    [HideInInspector]

    public void Start()
    {
        firstPostion = new Vector3(transform.position.x, transform.position.y, -5);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.white;
        Screen.DraggingOver = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = new Color(.94f, .94f, .94f, 1);
        Screen.DraggingOver = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ItemRepresenting)
        {
            Dragging = true;
            ItemImage.transform.SetParent(Screen.transform.parent);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Dragging = false;
        ItemImage.transform.SetParent(transform);
        if (Screen.DraggingOver)
        {
            int pos = Screen.DraggingOver.id;
            if (Screen.DraggingOver.ItemRepresenting)
            {
                Screen.DraggingOver.ItemRepresenting.InventoryPosition = ItemRepresenting.InventoryPosition;
            }
            Screen.DraggingOver.ItemImage.transform.position = Screen.DraggingOver.firstPostion;
            ItemImage.transform.position = firstPostion;
            if (ItemRepresenting)
            {
                ItemRepresenting.InventoryPosition = pos;
            }
            Screen.SetInventoryVisually(Screen.Inventory);
        }
        else
        {
            ItemImage.transform.position = firstPostion;
            Screen.SetInventoryVisually(Screen.Inventory);
        }
    }

    public void Update()
    {
        if(Dragging)
        {
            ItemImage.transform.position = Input.mousePosition;
        }
    }
}
