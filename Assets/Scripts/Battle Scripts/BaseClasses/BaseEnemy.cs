using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseClass {

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPERRARE,
        BOSS
    }

    public Rarity rarity;
}
