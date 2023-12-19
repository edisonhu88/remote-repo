using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus 
{
    public string playerName;
    public int killNum, deathNum, flagNum;
    public Sprite profileSprite, playerSprite;
    [HideInInspector] public string title;
    [HideInInspector] public string dateTime;


    public  static  bool CompareKill(PlayerStatus p1, PlayerStatus p2)
    {
        return p1.killNum > p2.killNum;
    }
    public static bool CompareDeath(PlayerStatus p1, PlayerStatus p2)
    {
        return p1.deathNum > p2.deathNum;
    }
    public static bool CompareFlag(PlayerStatus p1, PlayerStatus p2)
    {
        return p1.flagNum > p2.flagNum;
    }
}
