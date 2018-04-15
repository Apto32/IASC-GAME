using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIneract : MonoBehaviour {

    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;
    private GameObject tutMan;
    private new AudioSource audio;
    private bool isTalk = false;
    
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        tutMan = GameObject.FindGameObjectWithTag("Tut");
        tutMan.SetActive(false);
        
    }

    private void Update()
    {
        if(Input.GetButtonDown ("Interact") && currentInterObj)
        {
            
            //Check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
                audio.Play();
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
        //check to see if this object talks and has a message
        if(Input.GetButtonDown("Talk") && currentInterObj)
        {
            //tell the object to give its message
            currentInterObjScript.Talk();
            if (currentInterObjScript.message == "Press E to pick up items and P to use Ice Cream.")
            {
                if (isTalk == false)
                {
                    tutMan.SetActive(true);
                    isTalk = true;
                }
                else if (isTalk == true)
                {
                    tutMan.SetActive(false);
                    isTalk = false;
                }
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
