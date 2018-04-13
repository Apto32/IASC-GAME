using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer
{

	private string playerName;
	private int playerLevel;
	private BaseCharacterClass playerClass;
	private int W_staminia;			//health modifier for White
	private int Y_staminia;			//health modifier for Yellow
	private int G_staminia;			//health modifier for Green
	private int W_endurance;			//defense modifier White
	private int Y_endurance;			//defense modifier Yellow
	private int G_endurance;			//defense modifier Green 
	private int W_strength;			//physical ability modifier White
	private int Y_strength;			//physical ability modifier Yellow
	private int G_strength;			//physical ability modifier Green
	private int W_intellect;			//magical ability modifer White
	private int Y_intellect;			//magical ability modifer Yellow
	private int G_intellect;			//magical ability modifer Green
	private int W_agility;			//haste and Crit modifier
	private int Y_agility;			//haste and Crit modifier
	private int G_agility;			//haste and Crit modifier
	
	private int currentXP;
	private int requiredXP;

	private int statPointsToAllocate;


	public int StatPointsToAllocate
	{
		get { return statPointsToAllocate; }
		set { statPointsToAllocate = value; }
	}

	public string PlayerName
	{
		get { return playerName; }
		set { playerName = value; }
	}

	public int CurrentXP
	{
		get { return currentXP; }
		set { currentXP = value; }
	}

	public int RequiredXP
	{
		get { return requiredXP; }
		set { requiredXP = value; }
	}

	public int PlayerLevel
	{
		get { return playerLevel; }
		set { playerLevel = value; }
	}
	
	public  BaseCharacterClass PlayerClass
	{
		get { return playerClass; }
		set { playerClass = value; }
	}
	public int W_Staminia
	{
		get { return W_staminia; }
		set { W_staminia = value; }
	}
	public int Y_Staminia { get; set; }
	public int G_Staminia { get; set; }
	public int W_Endurance
	{
		get { return W_endurance; }
		set { W_endurance = value; }
	}
	public int Y_Endurance { get; set; }
	public int G_Endurance { get; set; }
	
	public int W_Strength
	{
		get { return W_strength; }
		set { W_strength = value; }
	}
	public int Y_Strength { get; set; }
	public int G_Strength { get; set; }
	public int W_Intellect
	{
		get { return W_intellect; }
		set { W_intellect = value; }
	}
	public int Y_Intellect { get; set; }
	public int G_Intellect { get; set; }
	public int W_Agility
	{
		get { return W_agility; }
		set { W_agility = value; }
	}
	public int Y_Agility { get; set; }
	public int G_Agility { get; set; }
}
