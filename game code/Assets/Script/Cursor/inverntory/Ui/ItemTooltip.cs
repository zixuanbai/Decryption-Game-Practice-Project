using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemNameText;

    public void UpdateItemNmae(ItemName itemName)
    {
        itemNameText.text = itemName switch
        {
        ItemName.key => "Just Key",
            ItemName.Ticket => "Just Ticket",
            _ => ""
        };
    }
}
