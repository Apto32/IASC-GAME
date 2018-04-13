using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class SaveInformation {

	public static void SaveAllInformation()
	{
		PlayerPrefs.SetInt("PLAYERLEVEL", GameInformation.PlayerLevel);
		PlayerPrefs.SetString("PLAYERNAME", GameInformation.PlayerName);
		PlayerPrefs.SetInt("W_STAMINIA", GameInformation.W_Staminia);
		PlayerPrefs.SetInt("W_ENDURANCE", GameInformation.W_Endurance);
		PlayerPrefs.SetInt("W_STRENGTH", GameInformation.W_Strength);
		PlayerPrefs.SetInt("W_INTELLECT", GameInformation.W_Intellect);
		PlayerPrefs.SetInt("W_AGILITY", GameInformation.W_Agility);
		
		PlayerPrefs.SetInt("Y_STAMINIA", GameInformation.Y_Staminia);
		PlayerPrefs.SetInt("Y_ENDURANCE", GameInformation.Y_Endurance);
		PlayerPrefs.SetInt("Y_STRENGTH", GameInformation.Y_Strength);
		PlayerPrefs.SetInt("Y_INTELLECT", GameInformation.Y_Intellect);
		PlayerPrefs.SetInt("Y_AGILITY", GameInformation.Y_Agility);
		
		PlayerPrefs.SetInt("G_STAMINIA", GameInformation.G_Staminia);
		PlayerPrefs.SetInt("G_ENDURANCE", GameInformation.G_Endurance);
		PlayerPrefs.SetInt("G_STRENGTH", GameInformation.G_Strength);
		PlayerPrefs.SetInt("G_INTELLECT", GameInformation.G_Intellect);
		PlayerPrefs.SetInt("G_AGILITY", GameInformation.G_Agility);

		Debug.Log("SAVED ALL INFO");
	}
}
