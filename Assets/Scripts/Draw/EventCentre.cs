using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCentre 
{
    private static EventCentre instance;
    public static EventCentre Instance
    {
        get { 
            if (instance == null)
            {
                instance = new EventCentre();
            }
            return instance;
        }
    }
    private  Dictionary<string,IeventInfo> _eventDic=new Dictionary<string, IeventInfo>(); //���ڴ洢�¼��б�
  
    public void  AddEventListener(string eventName, UnityAction action) //������Ϣ
    {
        if (_eventDic.ContainsKey(eventName))
        {
            (_eventDic[eventName] as EventInfo).actions += action;
        }
        else
        {
            _eventDic.Add(eventName,new EventInfo(action));
        }
    }

    public void  EventTrigger(string eventName) //֪ͨ��Ϣ�� �����¼�
    {
        if (_eventDic.ContainsKey(eventName)) {
            if ((_eventDic[eventName] as EventInfo).actions!=null)
            {
                (_eventDic[eventName] as EventInfo).actions.Invoke();
            }
        }
    }


    public void AddEventListener<T>(string eventName, UnityAction<T> action) //��Ӵ��������¼�����
    {
        if (_eventDic.ContainsKey(eventName))
        {
            (_eventDic[eventName] as EventInfo<T>).actions += action;
        }
        else
        {
            _eventDic.Add(eventName, new EventInfo<T>(action));
        }
    }

    public void EventTrigger<T>(string eventName,T info) //֪ͨ��Ϣ�� �����¼�
    {
        if (_eventDic.ContainsKey(eventName))
        {
            if ((_eventDic[eventName] as EventInfo<T>).actions != null)
            {
                (_eventDic[eventName] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }




    public void RemoveEventListener(string eventName, UnityAction action) //ȡ������
    {
        if (_eventDic.ContainsKey(eventName))
        {
            (_eventDic[eventName] as EventInfo).actions -= action;
        }
       
    }

    public void RemoveEventListener<T>(string eventName, UnityAction<T> action) //ȡ������
    {
        if (_eventDic.ContainsKey(eventName))
        {
            (_eventDic[eventName] as EventInfo<T>).actions -= action;
        }

    }
    public  void Clear()
    {
        _eventDic.Clear();
    }
    
}

public  interface IeventInfo
{

}

public  class  EventInfo : IeventInfo  //�¼��Ķ�����
{
    public UnityAction actions;
    public EventInfo( UnityAction action) {
        actions += action;

    }
}


public class EventInfo<T> : IeventInfo  //��װ���вη���
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;

    }
}