using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatAllocationModule
{

	private string[] statName = new string[15] {"Stamina", "Endurance", "Intellect", "Strength", "Agility","Stamina", "Endurance", "Intellect", "Strength", "Agility","Stamina", "Endurance", "Intellect", "Strength", "Agility"};
	private string[] statDescriptions = new string[15]{"Health Modifier", "Defense Modifier", "Ability Modifier", "Physical Damage Modifier", "Haste and Crit Modifier","Health Modifier", "Defense Modifier", "Ability Modifier", "Physical Damage Modifier", "Haste and Crit Modifier","Health Modifier", "Defense Modifier", "Ability Modifier", "Physical Damage Modifier", "Haste and Crit Modifier"};
	private bool[] statSelections = new bool[5];
	private int[] pointsToAllocate = new int[15]; //starting stat values for the chosen class,
	private int[] baseStatPoints = new int[15]; //starting stat values
	private int availPoints = 5;
	private bool didRunOnce = false;
	public void DisplayStatAllocationModule()
	{
		if (!didRunOnce)
		{
			RetrieveStatBaseStatPoints();
			didRunOnce = true;
		}
		DisplayStatToggles();
		DisplayStatIncreaseDecreaseButtons();
		
	}

	private void DisplayStatToggles()
	{
		for (int i = 0; i < statName.Length; i++)
		{
			statSelections[i] = GUI.Toggle(new Rect(10, 60 * i + 10, 50, 100), statSelections[i], statName[i]);
			GUI.Label(new Rect(100,60*i +10,50,50),pointsToAllocate[i].ToString());
			if (statSelections[i])
			{
				GUI.Label(new Rect(20, 60*i + 30, 150, 100),statDescriptions[i]);
			}
		}
	}

	private void DisplayStatIncreaseDecreaseButtons()
	{
		for (int i = 0; i < pointsToAllocate.Length; i++)
		{
			if (pointsToAllocate[i] > +baseStatPoints[i] && availPoints > 0)
			{
				if (GUI.Button(new Rect(200, 60 * i + 10, 50, 50), "+"))
				{
					pointsToAllocate[i] += 1;
					--availPoints;
				}
			}

			if (pointsToAllocate[i] > baseStatPoints[i])
			{
				if (GUI.Button(new Rect(260, 60 * i + 10, 50, 50), "-"))
				{
					pointsToAllocate[i] -= 1;
					++availPoints;
				}
			}
		}
	}

	private void RetrieveStatBaseStatPoints()
	{
		BaseCharacterClass cclass = GameInformation.PlayerClass;
		pointsToAllocate[0] = cclass.W_Staminia;
		baseStatPoints[0] = cclass.W_Staminia;
		pointsToAllocate[1] = cclass.W_Endurance;
		baseStatPoints[1] = cclass.W_Endurance;
		pointsToAllocate[2] = cclass.W_Intellect;
		baseStatPoints[2] = cclass.W_Intellect;
		pointsToAllocate[3] = cclass.W_Strength;
		baseStatPoints[3] = cclass.W_Strength;
		pointsToAllocate[4] = cclass.W_Agility;
		baseStatPoints[4] = cclass.W_Agility;
		pointsToAllocate[0] = cclass.Y_Staminia;
		baseStatPoints[0] = cclass.Y_Staminia;
		pointsToAllocate[1] = cclass.Y_Endurance;
		baseStatPoints[1] = cclass.Y_Endurance;
		pointsToAllocate[2] = cclass.Y_Intellect;
		baseStatPoints[2] = cclass.Y_Intellect;
		pointsToAllocate[3] = cclass.Y_Strength;
		baseStatPoints[3] = cclass.Y_Strength;
		pointsToAllocate[4] = cclass.Y_Agility;
		baseStatPoints[4] = cclass.Y_Agility;
		pointsToAllocate[0] = cclass.G_Staminia;
		baseStatPoints[0] = cclass.G_Staminia;
		pointsToAllocate[1] = cclass.G_Endurance;
		baseStatPoints[1] = cclass.G_Endurance;
		pointsToAllocate[2] = cclass.G_Intellect;
		baseStatPoints[2] = cclass.G_Intellect;
		pointsToAllocate[3] = cclass.G_Strength;
		baseStatPoints[3] = cclass.G_Strength;
		pointsToAllocate[4] = cclass.G_Agility;
		baseStatPoints[4] = cclass.G_Agility;
	}
}


