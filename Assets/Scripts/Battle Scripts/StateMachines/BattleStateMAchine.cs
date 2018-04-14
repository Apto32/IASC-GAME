using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMAchine : MonoBehaviour {

	public enum PerformAction
	{
		WAIT,
		TAKEACTION,
		PERFORMACTION
	}
	
	public PerformAction battleStates;

	public List<HandleTurn> PerformList = new List<HandleTurn>();
	public List<GameObject> HerosInBattle = new List<GameObject>();
	public List<GameObject> EnemiesInBattle = new List<GameObject>();


	public enum HeroGUI
	{
		ACTIVATE,
		WAITING,
		INPUT1,
		INPUT2,
		DONE
	}

	public HeroGUI HeroInput;

	public List<GameObject> HerosToManage = new List<GameObject>();
	private HandleTurn heroChoice;

	public GameObject enemyButton;
	public Transform Spacer;

	public GameObject AttackPanel;
	public GameObject EnemySelectPanel;
	
	// Use this for initialization
	void Start ()
	{
		battleStates = PerformAction.WAIT;
		EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
		HeroInput = HeroGUI.ACTIVATE;
		AttackPanel.SetActive(false);;
		EnemySelectPanel.SetActive(false);
		EnemyButtons();
	}
	
	// Update is called once per frame
	void Update () {
		switch (battleStates)
		{
			case(PerformAction.WAIT):
				if (PerformList.Count > 0)
				{
					battleStates = PerformAction.TAKEACTION;
				}
				break;
			case(PerformAction.TAKEACTION):
				GameObject performer = GameObject.Find(PerformList[0].Attacker);
				if (PerformList[0].Type == "Enemy")
				{
					EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
					ESM.HeroToAttack = PerformList[0].AttackersTarget;
					ESM.currentState = EnemyStateMachine.TurnState.ACTION;
				}
				if (PerformList[0].Type == "Hero")
				{
					HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
					HSM.EnemyToAttack = PerformList[0].AttackersTarget;
					HSM.currentState = HeroStateMachine.TurnState.ACTION;
				}
				break;
			case(PerformAction.PERFORMACTION):

				break;
		}

		switch (HeroInput)
		{
			case(HeroGUI.ACTIVATE):
				if (HerosToManage.Count > 0)
				{
					HerosToManage[0].transform.FindChild("Selector").gameObject.SetActive(true);
					heroChoice = new HandleTurn();
					AttackPanel.SetActive(true);
					HeroInput = HeroGUI.WAITING;
				}
				break;
			case(HeroGUI.WAITING):
					//idle
				break;
			case(HeroGUI.DONE):
				HeroInputDone();
				break;
		}
	}

	public void CollectActions(HandleTurn input)
	{
		PerformList.Add(input);
	}

	void EnemyButtons()
	{
		foreach (GameObject enemy in EnemiesInBattle)
		{
			GameObject newButton = Instantiate(enemyButton) as GameObject;
			EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

			EnemyStateMachine curEnemy = enemy.GetComponent<EnemyStateMachine>();
			Text buttonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
			buttonText.text = curEnemy.enemy.theName;

			button.EnemyPrefab = enemy;

			newButton.transform.SetParent(Spacer);
		}
	}

	public void Input1()//attack button
	{
		heroChoice.Attacker = HerosToManage[0].name;
		heroChoice.AttackersGameObject = HerosToManage[0];
		heroChoice.Type = "Hero";
		
		AttackPanel.SetActive(false);
		EnemySelectPanel.SetActive(true);
		
	}

	public void Input2(GameObject choosenEnemy)//enemy Selection
	{
		heroChoice.AttackersTarget = choosenEnemy;
		HeroInput = HeroGUI.DONE;
	}

	void HeroInputDone()
	{
		PerformList.Add((heroChoice));
		EnemySelectPanel.SetActive(false);
		HerosToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
		HerosToManage.RemoveAt(0);
		HeroInput = HeroGUI.ACTIVATE;
	}
}
