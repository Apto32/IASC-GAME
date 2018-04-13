using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CreateAPlayerGUI : MonoBehaviour {

	public enum CreateAPlayerStates
	{
		STATALLOCATION, // Update stats
		FINALSETUP 		//Add name
	}

	public static CreateAPlayerStates currentStates;
	private DisplayCreatePlayerFunctions displayFunctions = new DisplayCreatePlayerFunctions();
	
	// Use this for initialization
	void Start ()
	{
		GameInformation.PlayerClass = new BaseWYG();
		currentStates = CreateAPlayerStates.STATALLOCATION;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentStates)
		{
			case(CreateAPlayerStates.STATALLOCATION):
				break;
			case(CreateAPlayerStates.FINALSETUP):
				break;
		}
	}

	private void OnGUI()
	{
		if (currentStates == CreateAPlayerStates.STATALLOCATION)
		{
			//display stat allocation screen
			displayFunctions.DisplayStatAllocation();
		}

		if (currentStates == CreateAPlayerStates.FINALSETUP)
		{
			//display Hero name selection
			displayFunctions.DisplayFinalSetup();
		}
	}
}
