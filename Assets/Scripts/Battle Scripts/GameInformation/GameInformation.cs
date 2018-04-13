using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;

public class GameInformation : MonoBehaviour {
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	public static string PlayerName { get; set; }
	public static int PlayerLevel { get; set; }
	public static BaseCharacterClass PlayerClass { get; set; }
	public static int W_Staminia { get; set; }
	public static int W_Endurance { get; set; }
	public static int W_Strength { get; set; }
	public static int W_Intellect { get; set; }
	public static int W_Agility { get; set; }
	
	public static int CurrentXP { get; set; }
	public static int RequiredXP { get; set; }
	
	public static int Y_Staminia { get; set; }
	public static int Y_Endurance { get; set; }
	public static int Y_Strength { get; set; }
	public static int Y_Intellect { get; set; }
	public static int Y_Agility { get; set; }
	
	public static int G_Staminia { get; set; }
	public static int G_Endurance { get; set; }
	public static int G_Strength { get; set; }
	public static int G_Intellect { get; set; }
	public static int G_Agility { get; set; }
}
