using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestChangeLvl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject preloader;
    public int mineCount = 4;
    public int minPower = 100;
    public int maxPower = 100;
    public int lvl = 0;
    public void RequestLevel()
    {
        ChangeScene changeScene = preloader.GetComponent<ChangeScene>();
        changeScene.mineCount = mineCount;
        changeScene.minPower= minPower;
        changeScene.maxPower = maxPower;
        changeScene.lvl = lvl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
