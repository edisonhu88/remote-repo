using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using HutongGames.PlayMaker.Actions;

public class DrawMgr : MonoBehaviour
{
    public static DrawMgr instance;
    public int Level;
    public Text Levels;
    public Dictionary<int, string> LevelColor = new Dictionary<int, string>();
    public GameObject Circle;
    public GameObject Words;
    public  List<string> getWords = new List<string>();
    public static int Coin=250;
    public Text Moneys;
    private float TimeCount;


    private string url="http://101.43.75.39:5002?";

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //PlayerPrefs.SetInt("Level", 0);
        Screen.SetResolution(1080, 1920, false);
        Level = int.Parse(Levels.GetComponent<Text>().text) ;
        //Coin = int.Parse(Moneys.GetComponent<Text>().text) ;
        EventCentre.Instance.AddEventListener<int>("ScorceAdd", CheckNextLevel);
        EventCentre.Instance.AddEventListener("CountMinus", CountMinus);

    }

    private void CountMinus()
    {
        Circles.Instance.DeleleCircle();
        switch (Circles.Instance.Count)
        {   
            case 4:
                Circles.Instance.NewCircle(Util.ChooseStringList(Level)[1]);
                break;
            case 3:
                Circles.Instance.NewCircle(Util.ChooseStringList(Level)[2]);
                break;
            case 2:
                Circles.Instance.NewCircle(Util.ChooseStringList(Level)[3]);
                break;
            case 1:
                Circles.Instance.NewCircle(Util.ChooseStringList(Level)[4]);
                break;

        }



    }

    public void ShowWords()
    {
        Words.GetComponent<Text>().text = "";
        for (int i = 0; i < getWords.Count; i++)
        {
            Words.GetComponent<Text>().text += getWords[i];
        }
    }


   

    public void ClearCircle()
    {
        Words.GetComponent<Text>().text = "";
        getWords.Clear();
        UILineFactory.instance.lists.Clear();
        UILineFactory.instance.ClearLine();
        for (int i = 6; i < Circle.transform.childCount; i++)
        {
            Circle.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
       
      
      

    }



    public void Back()
    {
        SceneManager.LoadScene("start");
    }

    public  IEnumerator  NextLevel()
    {
        yield  return  new WaitForSeconds(2);
        PlayerPrefs.SetInt("Level", Level);
        SceneManager.LoadScene("Level"+(Level+1));

    }
   

    public void CheckNextLevel(int score)
    {
        if (Circles.Instance.Count==0)
        {
            StartCoroutine(NextLevel());

        }
        
    }

   

    private  IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        Moneys.GetComponentInParent<CoinShark>().enabled = false;
    }


    public   void MinusCoin()
    {
        if (Coin >= 50)
        {
            
            ClearCircle();
            SQLController.UpdateSQL(PlayerPrefs.GetString("username"), 0.05f);
            Moneys.GetComponent<Text>().text = (Coin + SQLController.InquireMysql<float>(PlayerPrefs.GetString("username"),"coins")).ToString();
        }
        else
        {
            Moneys.GetComponentInParent<CoinShark>().enabled = true;
            StartCoroutine(Wait());
            return;
        }
    }



    IEnumerator GetRequest(string url,System.DateTime time)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }


    public  void GetCoins()
    {
        // StartCoroutine(GetRequest(url));
       // url = url + ConvertDateTimeToInt(DateTime.Now);
        Application.OpenURL(url);
      
    }
   
         

    private  long ConvertDateTimeToInt(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        long t = (time.Ticks - startTime.Ticks) / 10000;  //除10000调整为13位   
        return t;
    }

    private void Update()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount >= 3)
        {
            try
            {
                var  coins = UpdateCoins(PlayerPrefs.GetString("username"));
                print(coins);
               Moneys.GetComponentInChildren<Text>().text = (Coin+coins).ToString();

            }
            catch (Exception)
            {
                TimeCount = 0;
                Moneys.GetComponentInChildren<Text>().text = (Coin).ToString();

                return;
            }
            finally { TimeCount = 0; }

        }
    }

    public  float UpdateCoins(string username)
    {
     
          return  SQLController.InquireMysql<float>(username,"coins") * 1000;
      

    }

  
}
