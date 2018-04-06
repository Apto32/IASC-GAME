using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

    public bool inventory;     //If true, this object can be stored in inventory
    public string itemType;    //this will tell what type of item this object is
    public bool talks;         //If true, then the object can talk to the player
    public string message;     //the message this object will give to the player


    public void DoInteraction()
    {
        //Picked up and put in inventory
        gameObject.SetActive(false);
    }

    public void Talk()
    {
        Debug.Log(message);
    }
}
