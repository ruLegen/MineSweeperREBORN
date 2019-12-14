using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;


public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    float score = 0;
    public GameObject endGame;
    void Start()
    {
        Drawer.onDrawEvent.AddListener(onDraw);
    }


    void onDraw(OnDrawMessage message)
    {
        Mine[] mines = message.mines;
        int squaredRadius = message.brushSize/ 2 * message.brushSize / 2;
        int startX = message.x - message.brushSize / 2;
        int startY = message.y - message.brushSize / 2;

        for (int x = startX; x < startX+ message.brushSize; x++)
        {
            for (int y = startY; y < startY+message.brushSize; y++)
            {
                //пому что он одномерный
                int u = x - message.x;
                int v = y - message.y;
                if (u * u + v * v < squaredRadius)
                {
                    foreach(Mine mine in mines)
                    {
                        if((int)mine.position.x == x && (int)mine.position.y == y)
                        {
                            Debug.Log("DEAD");
                            endGame.SetActive(true);

                        }
                    }
                }
            }

        }
    }
    // Update is called once per frame
}
