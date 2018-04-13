using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedCombatStateMachine : MonoBehaviour
{

	private bool hasAddedXp = false;
	
	public enum BattleStates
	{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN
	}

	private BattleStates currentState;
	
	// Use this for initialization
	void Start ()
	{
		hasAddedXp = false;
		currentState = BattleStates.START;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
		{
			case(BattleStates.START) :
				//Set Up Battle Function
				break;
			case(BattleStates.PLAYERCHOICE) :
				break;
			case(BattleStates.ENEMYCHOICE):
				break;
			case(BattleStates.LOSE):
				break;
			case(BattleStates.WIN):
				if (!hasAddedXp)
				{
					IncreaseEXP.AddExperience();
					hasAddedXp = true;
				}

				break;
		}
	}

	void OnGUI()
	{
		if (GUILayout.Button("NEXT STATE"))
		{
			if (currentState == BattleStates.START)
			{
				currentState = BattleStates.PLAYERCHOICE;
			} else if (currentState == BattleStates.PLAYERCHOICE)
			{
				currentState = BattleStates.ENEMYCHOICE;
			} else if (currentState == BattleStates.ENEMYCHOICE)
			{
				currentState = BattleStates.LOSE;
			} else if (currentState == BattleStates.LOSE)
			{
				currentState = BattleStates.WIN;
			} else if (currentState == BattleStates.WIN)
			{
				currentState = BattleStates.START;
			}
		}
	}
}
