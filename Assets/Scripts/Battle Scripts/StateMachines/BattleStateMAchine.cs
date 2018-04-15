using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMAchine : MonoBehaviour {

	public enum PerformAction
	{
		WAIT,
		TAKEACTION,
		PERFORMACTION,
		CHECKALIVE,
		WIN,
		LOSE
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

	public GameObject EnemyButton;
	public Transform Spacer;

	public GameObject AttackPanel;
	public GameObject EnemySelectPanel;
	public GameObject MagicPanel;
	
	//magic attack
	public Transform actionSpacer;
	public Transform magicSpacer;
	public GameObject actionButton;
	public GameObject magicButton;
	private List<GameObject> atkBtns = new List<GameObject>();
	
	private List<GameObject> enemyBtns = new List<GameObject>();

	private int numBattles = 0;
	private int requiredBattles = 5;
	
	//CREATE LIST OF SPAWNPOINTS
	public List<Transform> spawnPoints = new List<Transform>();

	void Awake()
	{
		for (int i = 0; i < GameManager.instance.enemyAmount; i++)
		{
			GameObject NewEnemy = Instantiate(GameManager.instance.enemiesToBattle[i], spawnPoints[i].position, Quaternion.Euler(90f, 90f, 270f)) as GameObject;
			NewEnemy.name = NewEnemy.GetComponent<EnemyStateMachine>().enemy.theName + " " + (i + 1);
			NewEnemy.GetComponent<EnemyStateMachine>().enemy.theName = NewEnemy.name;
			EnemiesInBattle.Add(NewEnemy);
		}
	}

	// Use this for initialization
	void Start ()
	{
		battleStates = PerformAction.WAIT;
		//EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
		HeroInput = HeroGUI.ACTIVATE;
		AttackPanel.SetActive(false);;
		EnemySelectPanel.SetActive(false);
		MagicPanel.SetActive(false);
		EnemyButtons();
	}
	
	// Update is called once per frame
	void Update ()
	{
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
					for (int i = 0; i < HerosInBattle.Count; i++)
					{
						if (PerformList[0].AttackersTarget == HerosInBattle[i])
						{
							ESM.HeroToAttack = PerformList[0].AttackersTarget;
							ESM.currentState = EnemyStateMachine.TurnState.ACTION;
							break;
						}
						else
						{
							PerformList[0].AttackersTarget = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
							ESM.HeroToAttack = PerformList[0].AttackersTarget;
							ESM.currentState = EnemyStateMachine.TurnState.ACTION;
						}
					}
					
				}
				if (PerformList[0].Type == "Hero")
				{
					if (heroChoice.choosenAttack.attackName == "Punch")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						HSM.EnemyToAttack = PerformList[0].AttackersTarget;
						HSM.currentState = HeroStateMachine.TurnState.ACTION;
					}

					else if (heroChoice.choosenAttack.attackName == "Super Slash")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						HSM.EnemyToAttack = PerformList[0].AttackersTarget;
						HSM.currentState = HeroStateMachine.TurnState.ACTION;
					}

					else if (heroChoice.choosenAttack.attackName == "Blade Dance")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						HSM.EnemyToAttack = PerformList[0].AttackersTarget;
						HSM.currentState = HeroStateMachine.TurnState.ACTION;
					}
					else if (heroChoice.choosenAttack.attackName == "Heal All")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						for ( int i = 0; i < HerosToManage.Count ; i++)
						{
							HSM.EnemyToAttack = HerosToManage[i];
							HSM.currentState = HeroStateMachine.TurnState.ACTION;
						}
					}
					else if (heroChoice.choosenAttack.attackName == "Heal One")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						HSM.EnemyToAttack = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
						HSM.currentState = HeroStateMachine.TurnState.ACTION;
					}
					else if (heroChoice.choosenAttack.attackName == "Dex Buff")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						HSM.EnemyToAttack = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
						HSM.currentState = HeroStateMachine.TurnState.ACTION;
					}
					else if (heroChoice.choosenAttack.attackName == "Agility Buff")
					{
						HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
						HSM.EnemyToAttack = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
						HSM.currentState = HeroStateMachine.TurnState.ACTION;
					}
				}
				break;
			case(PerformAction.PERFORMACTION):
				
				break;
			case(PerformAction.CHECKALIVE):
				if (HerosInBattle.Count < 1)
				{
					battleStates = PerformAction.LOSE;
					//lose the battle
				}else if (EnemiesInBattle.Count < 1)
				{
					battleStates = PerformAction.WIN;
					//win the battle
				}
				else
				{
					//call function
					clearAttackPanel();
					HeroInput = HeroGUI.ACTIVATE;
				}
				break;
			case(PerformAction.WIN):
				for (int i = 0; i < HerosInBattle.Count; i++)
				{
					HerosInBattle[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
					if (numBattles == requiredBattles)
					{
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.curATK += 10;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.curDEF += 10;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.curHP += 50;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.curMP += 20;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.baseATK += 10;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.baseDEF += 10;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.baseHP += 50;
						HerosInBattle[i].GetComponent<HeroStateMachine>().hero.baseMP += 20;
						requiredBattles += 5;
						numBattles = 0;
					}
					
					GameManager.instance.LoadSceneAfterBattle();
					GameManager.instance.InvCanvas.SetActive(true);
					GameManager.instance.gameState = GameManager.GameStates.WORLD_STATE;
					GameManager.instance.enemiesToBattle.Clear();

				}
				numBattles++;
				
				break;
			case(PerformAction.LOSE):

				break;
		}

		switch (HeroInput)
		{
			case(HeroGUI.ACTIVATE):
				if (HerosToManage.Count > 0)
				{
					HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
					heroChoice = new HandleTurn();
					AttackPanel.SetActive(true);
					//populate attack buttons
					CreateAttackButtons();
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

	public void EnemyButtons()
	{
		//cleanUp
		foreach (GameObject enemyBtn in enemyBtns)
		{
			Destroy(enemyBtn);
		}

		enemyBtns.Clear();
		//create buttons
		foreach (GameObject enemy in EnemiesInBattle)
		{
			GameObject newButton = Instantiate(EnemyButton) as GameObject;
			EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

			EnemyStateMachine curEnemy = enemy.GetComponent<EnemyStateMachine>();
			Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
			buttonText.text = curEnemy.enemy.theName;

			button.EnemyPrefab = enemy;

			newButton.transform.SetParent(Spacer);
			enemyBtns.Add(newButton);
		}
	}

	public void Input1()//attack button
	{
		heroChoice.Attacker = HerosToManage[0].name;
		heroChoice.AttackersGameObject = HerosToManage[0];
		heroChoice.Type = "Hero";
		heroChoice.choosenAttack = HerosToManage[0].GetComponent<HeroStateMachine>().hero.attacks[0];
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
		//clear attack panel
		clearAttackPanel();
		
		HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false);
		HerosToManage.RemoveAt(0);
		HeroInput = HeroGUI.ACTIVATE;
	}

	void clearAttackPanel()
	{
		EnemySelectPanel.SetActive(false);
		AttackPanel.SetActive(false);
		MagicPanel.SetActive(false);

		foreach (GameObject atkBtn in atkBtns)
		{
			Destroy(atkBtn);
		}
		atkBtns.Clear();
	}
	
	//create attack buttons
	void CreateAttackButtons()
   	{
  		GameObject AttackButton = Instantiate(actionButton) as GameObject;
   		Text attackButtonText = AttackButton.transform.Find("Text").gameObject.GetComponent<Text>();
   		attackButtonText.text = "Attack";
   		AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
   		AttackButton.transform.SetParent(actionSpacer, false);
  		atkBtns.Add(AttackButton);
		   
		GameObject MagicAttackButton = Instantiate(actionButton) as GameObject;
		Text MagicAttackButtonText = MagicAttackButton.transform.Find("Text").gameObject.GetComponent<Text>();
		MagicAttackButtonText.text = "Magic";
		MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());
		MagicAttackButton.transform.SetParent(actionSpacer, false);
		atkBtns.Add(MagicAttackButton);

		   if (HerosToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks.Count > 0)
		   {
			   foreach (BaseAttack magicAtk in HerosToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks)
			   {
				   GameObject MagicButton = Instantiate(magicButton) as GameObject;
				   Text MagicButtonText = MagicButton.transform.Find("Text").gameObject.GetComponent<Text>();
				   MagicButtonText.text = magicAtk.attackName;

				   AttackButton ATB = MagicButton.GetComponent<AttackButton>();
				   ATB.magicAttackToPerform = magicAtk;

				   MagicButton.transform.SetParent(magicSpacer, false);
				   atkBtns.Add(MagicButton);
			   }
		   }
		   else
		   {
			   MagicAttackButton.GetComponent<Button>().interactable = false;
		   }
	   }

	public void Input4(BaseAttack ChoosenMagic)//choosen Magic Attack
	{
		heroChoice.Attacker = HerosToManage[0].name;
		heroChoice.AttackersGameObject = HerosToManage[0];
		heroChoice.Type = "Hero";
		heroChoice.choosenAttack = ChoosenMagic;
		MagicPanel.SetActive(false);
		EnemySelectPanel.SetActive(true);
	}

	public void Input3()
	{
		AttackPanel.SetActive(false);
		MagicPanel.SetActive(true);
	}
}
