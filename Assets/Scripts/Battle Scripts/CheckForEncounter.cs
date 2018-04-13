using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEncounter : MonoBehaviour {

    public float enemy1 = 0.15f;
    public float enemy2 = 0.5f;
    public float enemy3 = 1f;
    public float probability = 0.05f;


    // Use this for initialization
    void Start() {
        InvokeRepeating("EnounterCheck", 0, 1.0f);

    }
	
	// Checks to see if there is a battle this second
	void EncounterCheck () {
		if (Random.value <= probability)
        {
            // Changes to battle phase while saving location in original scene, also resets the probability to 0.05

            probability = 0.05f;
        } else
        {
            //increases probability of the encounter happening
            probability += 0.05f;
        }
	}
}

