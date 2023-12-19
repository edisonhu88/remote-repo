using System;
using UnityEngine;
//�¼���ģ�� 5�����֣�  �¼���ӵ���ߡ��ࡿ ���¼���event�� �� �¼�����Ӧ�ߡ��ࡿ���¼����������ܵ�Լ���ķ������� �¼��Ķ��Ĺ�ϵ��+=��

public delegate void OrderEventHandler(Customer _customer,OrderEventArgs _e);//Ϊ�¼�׼����ί�У� ����ʹ��EventHandler��Ϊ��׺�� Լ���׳�
public class EventEx : MonoBehaviour
{
    
    Customer customer=new Customer();
    Waiter waiter =new Waiter();
    private void Start()
    {
        customer.OnOrder += waiter.TakeAction;//TakeAction ���¼�������
        customer.Order();
        customer.PayTheBill();
    }
}


public  class Customer //�¼���ӵ����
{
    public float Bill { get; set; }  //����ģ��         
    public void PayTheBill()
    {
        Debug.Log("i  have to pay :" + Bill);
    }
    private  OrderEventHandler orderEventHandler;//ί�����͵��ֶ�
    public  event OrderEventHandler OnOrder  //����һ���¼�
    {
        add
        {
            orderEventHandler += value; //δ��Ҫ���������¼�������
        }
        remove { 
            orderEventHandler -= value; 

        }
    }
          
    public  void Order() //��ͨ���� �� �����¼� 
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

public class Waiter  //�¼�����Ӧ��
{
    internal void TakeAction(Customer _customer, OrderEventArgs _e) //�¼�������
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
