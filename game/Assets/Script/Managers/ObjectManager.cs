using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneloadEvent += OnAfterSceneloadEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneloadEvent -= OnAfterSceneloadEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;

    }

    private void OnBeforeSceneUnloadEvent()
    {
        Item[] items = FindObjectsOfType<Item>();
        foreach (var item in items)
        {
            if (itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict[item.itemName] = item.gameObject.activeSelf;
            }
            else
            {
                itemAvailableDict.Add(item.itemName, item.gameObject.activeSelf);
            }
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void OnAfterSceneloadEvent()
    {
        Item[] items = FindObjectsOfType<Item>();
        foreach (var item in items)
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
            }
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemdetails, int arg2)
    {
       if (itemdetails != null)
        {
            itemAvailableDict[itemdetails.itemName] = false;
        }
    }
}
