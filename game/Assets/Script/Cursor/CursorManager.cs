using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x ,Input.mousePosition.y, 0));
    private ItemName currentItem;
    private bool canClick;
    private bool holdItem;

    private void OnEnable()
    {
        EventHandler.itemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.itemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (hand.gameObject.activeInHierarchy)
            hand.position = Input.mousePosition;

        if (InteractWithUI()) return;

        if(canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction( ObjectAtMousePosition().gameObject);
        }
    }
    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    private void OnItemSelectedEvent(ItemDetails ItemDetails, bool isSelected)
    {
        holdItem = isSelected;
        if (isSelected)
        {
            currentItem = ItemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);
    }

    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if (holdItem)
                    interactive?.CheckItem(currentItem);
                else
                    interactive?.EmptyClicked();
                break;

        }

    }



    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }

    private bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;

        return false;
    }
}
