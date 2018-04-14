using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	public Image ProgressBar;
	public GameObject Selector;
	//IeNumerator
	public GameObject EnemyToAttack;
	private bool actionStarted = false;
	private Vector3 startPosition;
	private float animSpeed = 10.0f;
	//dead
	private bool alive = true;
	
	// Use this for initialization
	void Start ()
	{
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
					for (int i = 0; i < BSM.PerformList.Count; i++)
					{
						if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
						{
							BSM.PerformList.Remove(BSM.PerformList[i]);
						}
					}
					//change color 
					this.gameObject.GetComponent<MeshRenderer>().material.shader = new Shader();
					//reset heroinput
					BSM.HeroInput = BattleStateMAchine.HeroGUI.ACTIVATE;
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
		
		//animate back to startposition
		Vector3 firstPosition = startPosition;
		while (moveTowardsStart(firstPosition)){yield return null;}
		//remove this performer for the list in BSM
		BSM.PerformList.RemoveAt(0);
		//reset the BSM -> WAIT
		BSM.battleStates = BattleStateMAchine.PerformAction.WAIT;
		actionStarted = false;
		//reset this enemy state
		curCooldown = 0.0f;
		currentState = TurnState.PROCESSING;

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
			currentState = TurnState.DEAD;
		}
	}
}
