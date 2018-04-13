using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp
{

    public int maxPlayerLevel = 10;
    
    public void LevelUpCharacter()
    {
        //Check to see if curXP is > reqXP
        if (GameInformation.CurrentXP > GameInformation.RequiredXP)
        {
            GameInformation.CurrentXP -= GameInformation.RequiredXP;
        }else
        {
            GameInformation.CurrentXP = 0;
        }

        if (GameInformation.PlayerLevel < maxPlayerLevel)
        {
            GameInformation.PlayerLevel += 1;
        }
        else
        {
            GameInformation.PlayerLevel = maxPlayerLevel;
        }
        //give player stat points
        //randomly decide to give items?
        //give move / ability
        //give money?
        //deteremine the next amount of required xp
        DetermineXPForNextLevel(GameInformation.PlayerLevel);
        
    }
    
    private int DetermineXPForNextLevel(int playerLevel)
    {
        playerLevel += 1;
        int levels = 10;
        float xpLevel1 = 500.0f;
        float xpLevel10 = 50000.0f;
        float temp1 = Mathf.Log(xpLevel10 / xpLevel1);
        float b = temp1 / (levels - 1);
        float temp2 = (Mathf.Exp(b) - 1);
        float a = (xpLevel1) / temp2;
        int oldxp = (int) (a - Mathf.Exp((float) b * (playerLevel - 1)));
        int newxp = (int) (a * Mathf.Exp((float) b * playerLevel));
        int temp = newxp - oldxp;
        temp = (int) Mathf.Round((float) temp / 10.0f) * 10;
        return temp;
    }
}