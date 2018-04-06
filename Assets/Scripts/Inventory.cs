using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public GameObject[] inventory = new GameObject[16];

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;
        //Find the first open slot in the inventory
        for (int i=0; i<inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
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
}
