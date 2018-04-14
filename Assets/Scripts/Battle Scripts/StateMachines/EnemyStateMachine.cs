using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyStateMachine : MonoBehaviour
{

	private BattleStateMAchine BSM;
    public BaseEnemy enemy;

	public enum TurnState
	{
		PROCESSING,
		CHOOSEACTION,
		WAITING,
		SELECTING,
		ACTION,
		DEAD,
	}
	
	

	public TurnState currentState;
	//for the ProgressBar
	private float curCooldown = 0f;
	private float max_cooldown = 5f;
	//this game object
	private Vector3 startposition;
	public GameObject Selector;
	//time for action stuff
	private bool actionStarted = false;
	public GameObject HeroToAttack;
	private float animSpeed = 10.0f;
	
	// Use this for initialization
	void Start () {
		currentState = TurnState.PROCESSING;
		Selector.SetActive(false);
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMAchine>();
		startposition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
		{
			case(TurnState.PROCESSING):
				UpgradeProgressBar();
				break;
			case(TurnState.CHOOSEACTION):
				ChooseAction();
				currentState = TurnState.WAITING;
				break;
			case(TurnState.WAITING):

				break;
			case(TurnState.SELECTING):

				break;
			case(TurnState.ACTION):
				StartCoroutine(TimeForAction());
				break;
			case(TurnState.DEAD):

				break;
		}
	}

		void UpgradeProgressBar()
		{
			curCooldown = curCooldown + Time.deltaTime;
			if (curCooldown >= max_cooldown)
			{
				currentState = TurnState.CHOOSEACTION;
			}
		}

	void ChooseAction()
	{
		HandleTurn myAttack = new HandleTurn();
		myAttack.Attacker = enemy.theName;
		myAttack.Type = "Enemy";
		myAttack.AttackersGameObject = this.gameObject;
		myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
		int num = Random.Range(0, enemy.attacks.Count);
		myAttack.choosenAttack = enemy.attacks[num];
		Debug.Log(this.gameObject.name + " has choosen " + myAttack.choosenAttack.attackName + " and do " +
		          myAttack.choosenAttack.attackDamage + " damage!");
		
		BSM.CollectActions(myAttack);
	}

	private IEnumerator TimeForAction()
	{
		if (actionStarted){
			yield break;
		}
		actionStarted = true;
		//animate the enemy near the hero to attack
		Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x+2.0f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
		while (moveTowardsEnemy(heroPosition)){yield return null;}
		//wait a bit
		yield return new WaitForSeconds(0.5f);
		//do damage
		DoDamage();
		//animate back to startposition
		Vector3 firstPosition = startposition;
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

	void DoDamage()
	{
		float calcDamage = enemy.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
		HeroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calcDamage);
	}
}
