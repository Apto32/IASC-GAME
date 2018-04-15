using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject[] inventory = new GameObject[16];
    public Button[] InventoryButtons = new Button[16];

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;
        //Find the first open slot in the inventory
        for (int i=0; i<inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                //update UI
                InventoryButtons[i].image.overrideSprite = item.GetComponent<SpriteRenderer>().sprite;
                Debug.Log(item.name + " was added");
                itemAdded = true;
                //Do something with the object
                item.SendMessage("DoInteraction");
                break;
            }
        }
        //Inventory full
        if (!itemAdded)
        {
            Debug.Log("Inventory full - Item not added");
        }
    }

    public GameObject FindItemByType(string itemType)
    {
        for (int i=0; i<inventory.Length; i++)
        {
            if(inventory[i] != null)
            {
                if(inventory[i].GetComponent<InteractionObject>().itemType == itemType)
                {
                    //We found an item of the type we were looking for
                    return inventory[i];
                }
            }
        }
        //item type not found
        return null;
    }

    public void RemoveItem(GameObject item)
    {
        for(int i=0; i<inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                //we fount the item - remove it
                inventory[i] = null;
                Debug.Log(item.name + " was removed from inventory.");
                //update UI
                InventoryButtons[i].image.overrideSprite = null;
                break;
            }
        }
    }
}
