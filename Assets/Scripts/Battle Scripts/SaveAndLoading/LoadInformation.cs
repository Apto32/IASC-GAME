using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInformation {

    public static void LoadAllInformation()
    {
        GameInformation.PlayerName = PlayerPrefs.GetString("PLAYERNAME");
        GameInformation.PlayerLevel = PlayerPrefs.GetInt("PLAYERLEVEL");
        
        GameInformation.W_Staminia = PlayerPrefs.GetInt("W_STAMINIA");
        GameInformation.W_Endurance = PlayerPrefs.GetInt("W_ENDURANCE");
        GameInformation.W_Strength = PlayerPrefs.GetInt("W_STRENGTH");
        GameInformation.W_Intellect = PlayerPrefs.GetInt("W_INTELLECT");
        GameInformation.W_Agility = PlayerPrefs.GetInt("W_AGILITY");
        
        GameInformation.Y_Staminia = PlayerPrefs.GetInt("Y_STAMINIA");
        GameInformation.Y_Endurance = PlayerPrefs.GetInt("Y_ENDURANCE");
        GameInformation.Y_Strength = PlayerPrefs.GetInt("Y_STRENGTH");
        GameInformation.Y_Intellect = PlayerPrefs.GetInt("Y_INTELLECT");
        GameInformation.Y_Agility = PlayerPrefs.GetInt("Y_AGILITY");
        
        GameInformation.G_Staminia = PlayerPrefs.GetInt("G_STAMINIA");
        GameInformation.G_Endurance = PlayerPrefs.GetInt("G_ENDURANCE");
        GameInformation.G_Strength = PlayerPrefs.GetInt("G_STRENGTH");
        GameInformation.G_Intellect = PlayerPrefs.GetInt("G_INTELLECT");
        GameInformation.G_Agility = PlayerPrefs.GetInt("G_AGILITY");

    }
    
}
