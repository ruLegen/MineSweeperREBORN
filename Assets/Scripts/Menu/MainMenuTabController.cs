using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MainMenuTabEvent : UnityEvent<int>
{

}


public class MainMenuTabController : MonoBehaviour
{
    public static MainMenuTabEvent onTabChangeRequest = new MainMenuTabEvent();
    public int startTab = 0;
    public void Start()
    {

        onTabChangeRequest.Invoke(startTab);
    } 
    public  void requestChangeTab(int tabID)
    {
        onTabChangeRequest.Invoke(tabID);
    }
}
