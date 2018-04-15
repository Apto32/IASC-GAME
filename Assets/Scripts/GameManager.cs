using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	
	//CLASS RANDOM MONSTER
	[System.Serializable]
	public class RegionData
	{
		public string regionName;
		public int maxAmountEnemies = 4;
		public string battleScene;
		public List<GameObject> possibleEnemies = new List<GameObject>();
	}

	public int curRegions;
	
	public List<RegionData> Regions = new List<RegionData>();

	//Hero
	public GameObject HeroCharacter;
	public GameObject InvCanvas;
	
	//Positions
	public Vector3 nextHeroPosition;
	public Vector3 lastHeroPosition; // Battle
	//Scenes
	public string SceneToLoad;
	public string lastScene; //Battle
	
	//BOOLS
	public bool isWalking = false;
	public bool canGetEncounter = false;
	public bool gotAttacked = false;
	
	//ENUM
	public enum GameStates
	{
		WORLD_STATE,
		BATTLE_STATE,
		IDLE
	}
	
	//BATTLE
	public List<GameObject> enemiesToBattle = new List<GameObject>();
	public int enemyAmount;

	public GameStates gameState;
	
	// Use this for initialization
	void Awake()
	{
		//check if instance exists
		if (instance == null)
		{
			//if not set the instance to this
			instance = this;
		}
		//if it exists but it is not this instance
		else if (instance != this)
		{
			//destroy it
			Destroy(gameObject);
		}

		//set this gameobject to be not destroyabel
		DontDestroyOnLoad(this);
		if (!GameObject.Find("White"))
		{
			GameObject Hero = Instantiate(HeroCharacter, Vector3.zero, Quaternion.identity) as GameObject;
			Hero.name = "White";
		}
	}

	void Update()
	{
		switch(gameState)
		{
			case(GameStates.WORLD_STATE):
				
				if (isWalking)
				{
					randomEncounter();
				}

				if (gotAttacked)
				{
					gameState = GameStates.BATTLE_STATE;
				}
				break;
			case(GameStates.BATTLE_STATE):
				//LOAD BATTLE SCENE
				InvCanvas.SetActive(false);
				StartBattle();
				//GO TO IDLE
				gameState = GameStates.IDLE;
				break;
			case (GameStates.IDLE):

				break;
		}	
	}

	public void LoadSceneAfterBattle()
	{
		SceneManager.LoadScene(lastScene);
	}

	private void randomEncounter()
	{
		if (isWalking && canGetEncounter)
		{
			if (Random.Range(0, 1000) < 7)
			{
				gotAttacked = true;
			}
		}
	}

	void StartBattle()
	{
		//AMOUNT OF ENEMIES
		enemyAmount = Random.Range(1, Regions[0].maxAmountEnemies + 1);
		//WHICH ENEMIES
		for (int i = 0; i < enemyAmount; i++)
		{
			enemiesToBattle.Add(Regions[curRegions].possibleEnemies[Random.Range(0, Regions[curRegions].possibleEnemies.Count)]);
		}
		//HERO
		lastHeroPosition = GameObject.Find("White").gameObject.transform.position;
		nextHeroPosition = lastHeroPosition;
		lastScene = SceneManager.GetActiveScene().name;
		//LOAD LEVEL
		SceneManager.LoadScene(Regions[curRegions].battleScene);
		//RESET HERO
		isWalking = false;
		gotAttacked = false;
		canGetEncounter = false;

	}
}
