using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineFactory : MonoBehaviour
{
    public static  UILineFactory instance;
    private void Awake()
    {
        instance = this;
    }
    public RectTransform rtUIParent;
    public GameObject prefabUILine;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public GameObject X;


    public List<GameObject> lists=new List<GameObject>();// Բ��������ѡ�е�����


    private void Start()
    {
        if (DrawMgr.instance.Level == 2)
        {
            prefabUILine.GetComponent<Image>().color = Color.green;
        }
        if (DrawMgr.instance.Level == 1)
        {
            prefabUILine.GetComponent<Image>().color = Color.yellow;
        }
        if (DrawMgr.instance.Level == 3)
        {
            prefabUILine.GetComponent<Image>().color = Color.blue;
        } if (DrawMgr.instance.Level == 4)
        {
            prefabUILine.GetComponent<Image>().color = Color.red;
        }
    }

    public  void ClearLine()
    {
        for (int i = 0; i < rtUIParent.childCount; i++)
        {
            if (rtUIParent.GetChild(i).name== "UILine")
            {
                Destroy(rtUIParent.GetChild(i).gameObject);
            }
        }
    }


    public void CreateLine()
    {
        if (lists.Count<2)
        {
            return;
        }
        for (int i = 0; i < lists.Count-1; i++)
        {
            startPosition = lists[i].gameObject.transform.position;
            endPosition = lists[i+1].gameObject.transform.position;
            GameObject line = Instantiate(prefabUILine);
            line.name = "UILine";
            line.transform.SetParent(rtUIParent.transform);

            var heading = endPosition - startPosition;
            var distance = heading.magnitude;
            var direction = heading / distance;

            RectTransform rtLine = line.GetComponent<RectTransform>();

            //�������½�
            rtLine.anchorMin = Vector2.zero;
            rtLine.anchorMax = Vector2.zero;

            //��������λ��
            Vector2 centerPos = new Vector2(startPosition.x + endPosition.x, startPosition.y + endPosition.y) / 2;
            rtLine.anchoredPosition = centerPos;

            //���������Ƕ�
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            line.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //yf th
            rtLine.sizeDelta = new Vector2(distance, 15.0f);
        }

        
    }

}
