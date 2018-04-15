using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{

	private BattleStateMAchine BSM;
    public BaseHero hero;

	public enum TurnState
	{
		PROCESSING,
		ADDTOLIST,
		WAITING,
		ACTION,
		DEAD,
	}

	public TurnState currentState;
	//for the ProgressBar
	private float curCooldown = 0f;
	private float max_cooldown = 5f;
	private Image ProgressBar;
	public GameObject Selector;
	//IeNumerator
	public GameObject EnemyToAttack;
	private bool actionStarted = false;
	private Vector3 startPosition;
	private float animSpeed = 10.0f;
	//dead
	private bool alive = true;
	//heroPanel
	private HeroPanelStats stats;
	public GameObject HeroPanel;
	private Transform HeroPanelSpacer;
	private Image HealthBar;

	private int turnCount = 0;
	
	// Use this for initialization
	void Start ()
	{
		//Find Spacer
		HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").transform.Find("HeroPanelSpacer");
		//create a panel and fill in info
		CreatHeroPanel();
		
		startPosition = transform.position;
		curCooldown = Random.Range(0, 2.5f);
		Selector.SetActive(false);
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMAchine>();
		currentState = TurnState.PROCESSING;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
		{
			case(TurnState.PROCESSING):
				UpgradeProgressBar();
				break;
			case(TurnState.ADDTOLIST):
				BSM.HerosToManage.Add(this.gameObject);
				currentState = TurnState.WAITING;
				break;
			case(TurnState.WAITING):
				//idel state
				break;
			case(TurnState.ACTION):
				StartCoroutine(TimeForAction());
				if (turnCount > 5)
				{
					for (int i = 0; i < BSM.HerosInBattle.Count; i++)
					{
						hero.curDEF = hero.baseDEF;
						hero.curATK = hero.baseATK;
					}
				}
				turnCount++;
				break;
			case(TurnState.DEAD):
				if (!alive)
				{
					return;
				}
				else
				{
					//change tag of hero
					this.gameObject.tag = "DeadHero";
					//not attackable by enemies
					BSM.HerosInBattle.Remove(this.gameObject);
					//not able to manage hero anymore
					BSM.HerosToManage.Remove(this.gameObject);
					//deactivate the selector
					Selector.SetActive(false);
					//reset GUI
					BSM.AttackPanel.SetActive(false);
					BSM.EnemySelectPanel.SetActive(false);
					//remove item from perform list
					if (BSM.HerosInBattle.Count > 0)
					{
						for (int i = 0; i < BSM.PerformList.Count; i++)
						{
							if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
							{
								BSM.PerformList.Remove(BSM.PerformList[i]);
							}

							if (BSM.PerformList[i].AttackersTarget = this.gameObject)
							{
								BSM.PerformList[i].AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
							}
						}
					}

					//change color 
					this.gameObject.SetActive(false);
					//reset heroinput
					BSM.battleStates = BattleStateMAchine.PerformAction.CHECKALIVE;
					alive = false;
				}
				break;

		}
	}

	void UpgradeProgressBar()
	{
		curCooldown = curCooldown + Time.deltaTime;
		float calcCooldown = curCooldown / max_cooldown;
		ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calcCooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
		if (curCooldown >= max_cooldown)
		{
			currentState = TurnState.ADDTOLIST;
		}
	}
	
	private IEnumerator TimeForAction()
	{
		if (actionStarted){
			yield break;
		}
		actionStarted = true;
		//animate the enemy near the hero to attack
		Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x-2.0f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
		while (moveTowardsEnemy(enemyPosition)){yield return null;}
		//wait a bit
		yield return new WaitForSeconds(0.5f);
		//do damage
		if (BSM.PerformList[0].choosenAttack.attackName == "Punch")
		{
			doDamage();	
		}else if (BSM.PerformList[0].choosenAttack.attackName == "Super Slash")
		{
			doDamage();	
		}else if (BSM.PerformList[0].choosenAttack.attackName == "Blade Dance")
		{
			doDamage();	
		}else if (BSM.PerformList[0].choosenAttack.attackName == "Heal One")
		{
			doHeal();
		}else if (BSM.PerformList[0].choosenAttack.attackName == "Heal All")
		{
			doHeal();
		}else if (BSM.PerformList[0].choosenAttack.attackName == "Dex Buff")
		{
			doBuff();
		}else if (BSM.PerformList[0].choosenAttack.attackName == "Agility Buff")
		{
			doBuff();
		}
		
		
		//animate back to startposition
		Vector3 firstPosition = startPosition;
		while (moveTowardsStart(firstPosition)){yield return null;}
		//remove this performer for the list in BSM
		BSM.PerformList.RemoveAt(0);
		//reset the BSM -> WAIT
		if (BSM.battleStates != BattleStateMAchine.PerformAction.WIN &&
		    BSM.battleStates != BattleStateMAchine.PerformAction.LOSE)
		{
			BSM.battleStates = BattleStateMAchine.PerformAction.WAIT;
			//reset this enemy state
            curCooldown = 0.0f;
           	currentState = TurnState.PROCESSING;
		}
		actionStarted = false;
		

	}

	private bool moveTowardsEnemy(Vector3 target)
	{
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}
	private bool moveTowardsStart(Vector3 target)
	{
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}

	public void TakeDamage(float getDamageAmount)
	{
		hero.curHP -= getDamageAmount;
		if (hero.curHP <= 0)
		{
			hero.curHP = 0;
			currentState = TurnState.DEAD;
		}
		UpdateHealthBar();
	}

	void CreatHeroPanel()
	{
		HeroPanel = Instantiate(HeroPanel) as GameObject;
		stats = HeroPanel.GetComponent<HeroPanelStats>();
		stats.HeroName.text = hero.theName;
		HealthBar = stats.heroHP;
		HealthBar.transform.localScale = new Vector3(Mathf.Clamp(hero.curHP/hero.baseHP, 0, 1), HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
		ProgressBar = stats.ProgressBar;
		HeroPanel.transform.SetParent(HeroPanelSpacer, false);
	}

	void UpdateHealthBar()
	{
		HealthBar.transform.localScale = new Vector3(Mathf.Clamp(hero.curHP/hero.baseHP, 0, 1), HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
	}
	
	void doDamage()
	{
		float calcDamage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage - ((EnemyToAttack.GetComponent<EnemyStateMachine>().enemy.curDEF)/5);
		if (calcDamage <= 0)
		{
			calcDamage = 0f;
		}
		EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calcDamage);
	}

	void doHeal()
	{
		float calcDamage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
		EnemyToAttack.GetComponent<HeroStateMachine>().Heal(calcDamage);
	}

	void Heal(float getHealAmount)
	{
		hero.curHP += getHealAmount;
		if (hero.curHP >= hero.baseHP)
		{
			hero.curHP = hero.baseHP;
		}
		UpdateHealthBar();
	}

	void doBuff()
	{
		float calcBuff = (hero.curATK/5) + BSM.PerformList[0].choosenAttack.attackDamage;
		EnemyToAttack.GetComponent<HeroStateMachine>().Buff(calcBuff);
	}

	void Buff(float buffAmount)
	{
		if (BSM.PerformList[0].choosenAttack.attackName == "Dex Buff")
		{
			hero.curDEF += buffAmount;
		}
		if (BSM.PerformList[0].choosenAttack.attackName == "Agility Buff")
		{
			hero.curATK += buffAmount;
		}
	}
}
