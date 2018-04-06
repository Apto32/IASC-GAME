﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIneract : MonoBehaviour {

    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(Input.GetButtonDown ("Interact") && currentInterObj)
        {
            //Check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }
        }
        //use an item
        if(Input.GetButtonDown("Use Item"))
        {
            //check inventory for an item
            GameObject iceCream = inventory.FindItemByType("Ice Cream");
            if (iceCream != null)
            {
                //use the item - apply its effect
                //remove item from inventory
                inventory.RemoveItem(iceCream);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InterObject"))
        {
            Debug.Log(collision.name);
            currentInterObj = collision.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InterObject"))
        {
            if (collision.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
    }
}
