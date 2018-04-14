using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{

	public GameObject EnemyPrefab;
	private bool showSelector;

	public void selectEnemy()
	{
		GameObject.Find("BattleManager").GetComponent<BattleStateMAchine>().Input2(EnemyPrefab); // save input of enemy prefab
	}

	public void HideSelector()
	{
		EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(false);
	}
	
	public void ShowSelector()
	{
		EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(true);
	}
}
