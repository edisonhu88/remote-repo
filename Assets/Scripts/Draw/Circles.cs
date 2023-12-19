using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circles : MonoBehaviour
{
    public static Circles Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject Area1;
    public GameObject Area2;
    public GameObject Area3;
    public GameObject Area4;
    public GameObject Area5;
    public Text Counts;
    public int Count;
    public GameObject prefabs;

    private void Start()
    {
        NewCircle(Util.ChooseStringList(DrawMgr.instance.Level)[0]);

      
    }
 
    public void NewCircle(string[] x)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject y;
            y = Instantiate(prefabs, transform.GetChild(i).position, Quaternion.identity, transform);
            y.GetComponentInChildren<Text>().text = x[i];
        }
    }

  

    public  void DeleleCircle() {
        for (int i = 6; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
