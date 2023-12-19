using System;
using UnityEngine;
//事件的模型 5个部分：  事件的拥有者【类】 ，事件【event】 ， 事件的响应者【类】，事件处理器【受到约束的方法】， 事件的订阅关系【+=】

public delegate void OrderEventHandler(Customer _customer,OrderEventArgs _e);//为事件准备的委托， 命名使用EventHandler作为后缀， 约定俗成
public class EventEx : MonoBehaviour
{
    
    Customer customer=new Customer();
    Waiter waiter =new Waiter();
    private void Start()
    {
        customer.OnOrder += waiter.TakeAction;//TakeAction 是事件处理器
        customer.Order();
        customer.PayTheBill();
    }
}


public  class Customer //事件的拥有者
{
    public float Bill { get; set; }  //属性模版         
    public void PayTheBill()
    {
        Debug.Log("i  have to pay :" + Bill);
    }
    private  OrderEventHandler orderEventHandler;//委托类型的字段
    public  event OrderEventHandler OnOrder  //声明一个事件
    {
        add
        {
            orderEventHandler += value; //未来要传进来的事件处理器
        }
        remove { 
            orderEventHandler -= value; 

        }
    }
          
    public  void Order() //普通方法 ， 调用事件 
    {
        if (orderEventHandler != null)
        {
            OrderEventArgs e = new OrderEventArgs();
            e.coffeeName = "Mocha";
            e.coffeeSize = "Tall";
            e.coffeePrice = 28;


            orderEventHandler(this, e);
        }
    }
}

public  class OrderEventArgs:EventArgs 
{
    public string coffeeName { get; set; }
    public string coffeeSize { get; set; }
    public float coffeePrice { get; set; }

}

public class Waiter  //事件的响应者
{
    internal void TakeAction(Customer _customer, OrderEventArgs _e) //事件处理器
    {
        float finalPrice = 0;

        switch (_e.coffeeSize)
        {
            case "Tall":
                finalPrice =_e.coffeePrice;
                break;
            case "Grand":
                finalPrice = _e.coffeePrice+3;
                break;
            case "Venti":
                finalPrice = _e.coffeePrice + 6;
                break;
        }
        _customer.Bill += finalPrice;
    }
}
