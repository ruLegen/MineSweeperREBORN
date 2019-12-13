using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabListener : MonoBehaviour
{
    public int tabID = 0;
    public TabListener()
    {
        Debug.Log("Contructor");
        MainMenuTabController.onTabChangeRequest.AddListener(checkTabRequest);

    }
    public void Awake()
    {
        Debug.Log("Registered "+tabID.ToString());
    }

    void checkTabRequest(int id)
    {
        if (id == this.tabID)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

    }
}
