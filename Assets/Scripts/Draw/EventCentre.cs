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
    private  Dictionary<string,IeventInfo> _eventDic=new Dictionary<string, IeventInfo>(); //用于存储事件列表
  
    public void  AddEventListener(string eventName, UnityAction action) //订阅消息
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

    public void  EventTrigger(string eventName) //通知消息， 触发事件
    {
        if (_eventDic.ContainsKey(eventName)) {
            if ((_eventDic[eventName] as EventInfo).actions!=null)
            {
                (_eventDic[eventName] as EventInfo).actions.Invoke();
            }
        }
    }


    public void AddEventListener<T>(string eventName, UnityAction<T> action) //添加带参数的事件监听
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

    public void EventTrigger<T>(string eventName,T info) //通知消息， 触发事件
    {
        if (_eventDic.ContainsKey(eventName))
        {
            if ((_eventDic[eventName] as EventInfo<T>).actions != null)
            {
                (_eventDic[eventName] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }




    public void RemoveEventListener(string eventName, UnityAction action) //取消订阅
    {
        if (_eventDic.ContainsKey(eventName))
        {
            (_eventDic[eventName] as EventInfo).actions -= action;
        }
       
    }

    public void RemoveEventListener<T>(string eventName, UnityAction<T> action) //取消订阅
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

public  class  EventInfo : IeventInfo  //事件的订阅者
{
    public UnityAction actions;
    public EventInfo( UnityAction action) {
        actions += action;

    }
}


public class EventInfo<T> : IeventInfo  //封装的有参方法
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;

    }
}