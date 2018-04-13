using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using JetBrains.Annotations;
using UnityEngine;

public static class IncreaseEXP
{
	private static float xpToGive;
	private static LevelUp levelUpScript = new LevelUp();
	
	public static void AddExperience()
	{
		xpToGive = GameInformation.PlayerLevel * 100;
		GameInformation.CurrentXP += (int)xpToGive;
		CheckToSeeIfPlayerLeveled();
	}

	private static void CheckToSeeIfPlayerLeveled()
	{
		if (GameInformation.CurrentXP >= GameInformation.RequiredXP)
		{
			//Then the Player leveld up
			levelUpScript.LevelUpCharacter();
			//CREATE LEVEL UP SCRIPT
		}
	}

}
