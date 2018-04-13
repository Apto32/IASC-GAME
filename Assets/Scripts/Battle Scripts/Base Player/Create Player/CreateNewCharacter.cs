using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNewCharacter : MonoBehaviour
{

	private BasePlayer newPlayer;
	private bool isWYGClass = true;
	private string playerName = "Enter Name";

	// Use this for initialization
	void Start()
	{
		newPlayer = new BasePlayer();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnGUI()
	{
		if (GUILayout.Button("START"))
		{
			if (isWYGClass)
			{
				newPlayer.PlayerClass = new BaseWYG();
			}

			CreateNewPlayer();
			StoreNewPlayerInfo();
			SaveInformation.SaveAllInformation();
		}

		if (GUILayout.Button("Load"))
		{
			SceneManager.LoadScene("Level_1");
		}
	}
	private void StoreNewPlayerInfo()
	{
		GameInformation.PlayerName = newPlayer.PlayerName;
		GameInformation.PlayerLevel = newPlayer.PlayerLevel;
		GameInformation.W_Staminia = newPlayer.W_Staminia;
		GameInformation.W_Endurance = newPlayer.W_Endurance;
		GameInformation.W_Strength = newPlayer.W_Strength;
		GameInformation.W_Intellect = newPlayer.W_Intellect;
		GameInformation.W_Agility = newPlayer.W_Agility;
		GameInformation.Y_Staminia = newPlayer.Y_Staminia;
		GameInformation.Y_Endurance = newPlayer.Y_Endurance;
		GameInformation.Y_Strength = newPlayer.Y_Strength;
		GameInformation.Y_Intellect = newPlayer.Y_Intellect;
		GameInformation.Y_Agility = newPlayer.Y_Agility;
		GameInformation.G_Staminia = newPlayer.G_Staminia;
		GameInformation.G_Endurance = newPlayer.G_Endurance;
		GameInformation.G_Strength = newPlayer.G_Strength;
		GameInformation.G_Intellect = newPlayer.G_Intellect;
		GameInformation.G_Agility = newPlayer.G_Agility;

	}

	private void CreateNewPlayer()
	{
		newPlayer.PlayerLevel = 1;
		newPlayer.PlayerName = playerName;
		newPlayer.W_Staminia = newPlayer.PlayerClass.W_Staminia;
		newPlayer.W_Endurance = newPlayer.PlayerClass.W_Endurance;
		newPlayer.W_Strength = newPlayer.PlayerClass.W_Strength;
		newPlayer.W_Intellect = newPlayer.PlayerClass.W_Intellect;
		newPlayer.W_Agility = newPlayer.PlayerClass.W_Agility;
		newPlayer.Y_Staminia = newPlayer.PlayerClass.Y_Staminia;
		newPlayer.Y_Endurance = newPlayer.PlayerClass.Y_Endurance;
		newPlayer.Y_Strength = newPlayer.PlayerClass.Y_Strength;
		newPlayer.Y_Intellect = newPlayer.PlayerClass.Y_Intellect;
		newPlayer.Y_Agility = newPlayer.PlayerClass.Y_Agility;
		newPlayer.G_Staminia = newPlayer.PlayerClass.G_Staminia;
		newPlayer.G_Endurance = newPlayer.PlayerClass.G_Endurance;
		newPlayer.G_Strength = newPlayer.PlayerClass.G_Strength;
		newPlayer.G_Intellect = newPlayer.PlayerClass.G_Intellect;
		newPlayer.G_Agility = newPlayer.PlayerClass.G_Agility;
		Debug.Log("Player Name: " + newPlayer.PlayerName);
		Debug.Log("player class: " + newPlayer.PlayerClass.CharacterClassName);
		Debug.Log("player Level: " + newPlayer.PlayerLevel);
		Debug.Log("player Staminia: " + newPlayer.W_Staminia);
		Debug.Log("player Endurance: " + newPlayer.W_Endurance);
		Debug.Log("player Strength: " + newPlayer.W_Strength);
		Debug.Log("player Intellect: " + newPlayer.W_Intellect);
		Debug.Log("player Agility: " + newPlayer.W_Agility);

	}
};
