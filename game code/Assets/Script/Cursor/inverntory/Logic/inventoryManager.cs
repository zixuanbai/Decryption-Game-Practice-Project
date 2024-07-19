using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : Singleton<inventoryManager>
{

    public ItemDataList_SO itemData;
    
    [SerializeField]private List<ItemName> itemList = new List<ItemName>();


    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneloadEvent += OnAfterScenLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneloadEvent -= OnAfterScenLoadedEvent;
    }

    private void OnAfterScenLoadedEvent()
    {
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
        else
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);

        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
            var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);

        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
    }

    public void AddItem(ItemName itemName)
    {

        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //ToDo ui
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }
    }

    private int GetItemIndex(ItemName itemName)
    {
        for(int i= 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }
        return -1;
    }
}
