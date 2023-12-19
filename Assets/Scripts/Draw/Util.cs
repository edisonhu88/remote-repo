using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    private static Util instance;
    public static Util Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Util();
            }
            return instance;
        }
    }


    public static List<string[]> LevelCircle3 = new List<string[]>()
    {
        new string[] { "E", "O", "M", "A", "D", "R" },
        new string[] { "O", "P", "E", "R", "A", "I" },
        new string[] { "A", "O", "S", "T", "H", "I" },
        new string[] { "L", "O", "I", "Y", "R", "B" },
        new string[] { "S", "O", "I", "C", "E", "T" },
        new string[] { "P", "O", "B", "D", "R", "A" },
    };
    public static List<string[]> LevelCircle2 = new List<string[]>()
    {
        new string[] { "I", "O", "P", "A", "D", "R" },
        new string[] { "J", "I", "O", "N", "R", "M" },
        new string[] { "R", "O", "I", "Y", "W", "B" },
        new string[] { "S", "A", "D", "P", "E", "L" },
        new string[] { "O", "P", "Y", "R", "A", "I" },
        new string[] { "P", "O", "H", "Z", "R", "A" },
    };

    public static List<string[]> LevelCircle1 = new List<string[]>()
    {
        new string[] { "T", "O", "G", "A", "U", "Y" },
        new string[] { "S", "D", "O", "N", "E", "R" },
        new string[] { "R", "O", "F", "Y", "P", "T" },
        new string[] { "R", "A", "I", "P", "Y", "K" },
        new string[] { "S", "E", "L", "R", "A", "I" },
    };
    public static List<string[]> LevelCircle4 = new List<string[]>()
    {
        new string[] { "T", "A", "G", "S", "N", "I" },
        new string[] { "O", "L", "F", "M", "C", "E" },
        new string[] { "U", "A", "E", "L", "N", "S" },
        new string[] { "R", "M", "I", "D", "Y", "J" },
        new string[] { "L", "U", "B", "K", "A", "F" },
    };

    public static  List<string[]> ChooseStringList(int x)
    {
        switch (x)
        {
            case 3:
                return LevelCircle3;
                break;
            case 2:
                return LevelCircle2;
                break;
            case 1:
                return LevelCircle1;
                break;
            case 4:
                return LevelCircle4;
                break;
        }


        return null;
    }
    
}
