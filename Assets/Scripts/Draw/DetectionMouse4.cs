using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class DetectionMouse4 : MonoBehaviour, IPointerClickHandler
{
    public static DetectionMouse4 Instance;
    private void Awake()
    {
        Instance = this;
    }
  
    public void OnPointerClick(PointerEventData eventData)
    {
        Circles.Instance.Count = int.Parse(Circles.Instance.Counts.GetComponent<Text>().text);
        UILineFactory.instance.lists.Add(gameObject);
        UILineFactory.instance.CreateLine();
        GetComponent<Image>().color = new Color32(220, 60, 60, 255);
        DrawMgr.instance.getWords.Add(GetComponentInChildren<Text>().text);
        DrawMgr.instance.ShowWords();
        UpdateCircle();

    }



    public void   UpdateCircle()
    {
        switch (DrawMgr.instance.Words.GetComponent<Text>().text)
        {
            case "SAIN":
                StartCoroutine(WhichArea(Circles.Instance.Area1));
                break;
            case "FLMECO":
                StartCoroutine(WhichArea(Circles.Instance.Area2));
                break;
            case "ENSUA":
                StartCoroutine(WhichArea(Circles.Instance.Area3));
                break;
            case "MDRI":
                StartCoroutine(WhichArea(Circles.Instance.Area4));
                break;
            case "BUL":
                StartCoroutine(WhichArea(Circles.Instance.Area5));
                break;


        }

    }

    private IEnumerator  WhichArea(GameObject Area)
    {
        FlyTo(Area);
        DrawMgr.instance.ClearCircle();
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < Area.transform.childCount; i++)
        {
            Area.transform.GetChild(i).GetComponent<Image>().color = new Color32(220, 60, 60, 255);
            Area.transform.GetChild(i).GetChild(0).gameObject.active = true;
        }
        Circles.Instance.Count--;
        Circles.Instance.Counts.GetComponent<Text>().text = Circles.Instance.Count.ToString();
        EventCentre.Instance.EventTrigger("CountMinus");
        EventCentre.Instance.EventTrigger<int>("ScorceAdd", 1);
    }



    private  void FlyTo(GameObject f)
    {
        for (int i = 0; i < UILineFactory.instance.lists.Count; i++)
        {
            UILineFactory.instance.lists[i].transform.DOMove(f.transform.GetChild(i).position, 1.5f);
            Destroy(UILineFactory.instance.lists[i].transform.GetComponent<Image>());

        }


    }



}
