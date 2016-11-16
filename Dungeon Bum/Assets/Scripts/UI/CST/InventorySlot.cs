using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Assets.Scripts.Entity.Items;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Item ItemRepresenting;
    public InventoryScreen Screen;
    public Image ItemImage;
    public int id;
    [HideInInspector]
    public Vector3 firstPostion;
    private Vector3 initialMousePosition;
    [HideInInspector]
    public bool CheckForDrag = false;

    public void Start()
    {
        firstPostion = new Vector3(transform.position.x, transform.position.y, -5);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.white;
        Screen.DraggingOver = this;
        if (Screen.Popup != null && !Screen.Dragging)
        {
            Destroy(Screen.Popup.gameObject);
            Screen.Popup = null;
        }
        if (ItemRepresenting && !Screen.Dragging)
        {
            GameObject go = Resources.Load("Items/UI/UIHoverPopup") as GameObject;
            ItemHoverPopup pop = (Instantiate(go) as GameObject).GetComponent<ItemHoverPopup>();
            pop.transform.SetParent(Screen.transform);
            pop.PopupText.text = ItemRepresenting.UIName;
            pop.PopupText.color = ItemRepresenting.GetTextColor();

            Screen.Popup = pop;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = new Color(.94f, .94f, .94f, 1);
        Screen.DraggingOver = null;
        if (Screen.Popup != null && !Screen.Dragging)
        {
            Destroy(Screen.Popup.gameObject);
            Screen.Popup = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CheckForDrag = true;
        initialMousePosition = Input.mousePosition;
        Screen.CurrentlyDragging = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Screen.Dragging = false;
        ItemImage.transform.SetParent(transform);
        Screen.CurrentlyDragging = null;
        if (Screen.DraggingOver)
        {
            int pos = Screen.DraggingOver.id;
            if (Screen.DraggingOver.ItemRepresenting && ItemRepresenting)
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
        if (Screen.Dragging && Screen.CurrentlyDragging == this)
        {
            ItemImage.transform.position = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && CheckForDrag)
        {
            if (Input.mousePosition != initialMousePosition)
            {
                if (ItemRepresenting)
                {
                    Screen.Dragging = true;
                    ItemImage.transform.SetParent(Screen.transform.parent);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CheckForDrag = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("click");
    }
}
