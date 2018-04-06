using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

    public bool inventory;     //If true, this object can be stored in inventory

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void DoInteraction()
    {
        //Picked up and put in inventory
        gameObject.SetActive(false);
    }
}
