using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinShark : MonoBehaviour
{
    public bool isEnable = true;
    public Vector3 shakedegree = Vector3.one * 0.1f;
    public float addDegreeTime = 1;

    private Vector3 originPosition;
    private float timeCount;

    void Start()
    {
        originPosition = transform.localPosition;
    }

    void Update()
    {
        if (isEnable)
        {
            if (timeCount < addDegreeTime)
                timeCount += Time.deltaTime;
            else
                timeCount = addDegreeTime;
            transform.localPosition = new Vector3(GetX(), GetY(), GetZ());
        }
        else
        {
            if (timeCount > 0)
            {
                timeCount -= Time.deltaTime;
                transform.localPosition = new Vector3(GetX(), GetY(), GetZ());
            }
            else
            {
                transform.localPosition = originPosition;
            }
        }
    }

    private float GetX()
    {
        return originPosition.x + Random.Range(-20 * timeCount * shakedegree.x, timeCount * shakedegree.x);
    }
    private float GetY()
    {
        return originPosition.y + Random.Range(-20 * timeCount * shakedegree.y, timeCount * shakedegree.y);
    }
    private float GetZ()
    {
        return originPosition.z + Random.Range(-20 * timeCount * shakedegree.y, timeCount * shakedegree.y);
    }
}
