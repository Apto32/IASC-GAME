using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{

	public string attackName; //name
	public float attackDamage; //Base Damage
	public float attackCost; // If it is a Special Attack (Mana Cost)
	
}
