using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class GameLogic : MonoBehaviour
{
    int score = 0;
    public GameObject endGame;
    public GameObject winGame;
    public Text percentFilled;
    int filled = 0;
    int totalLength = 0;
    public Text scoreText;
    public float percentWin = 70;
    private float[,] visitedMap = null; 
    void Start()
    {
        Drawer.onDrawEvent.AddListener(onDraw);
    }


    void onDraw(OnDrawMessage message)
    {
        if (visitedMap == null)
        {
            visitedMap = message.normilized;
            totalLength = visitedMap.GetLength(0) + visitedMap.GetLength(1);
        }
        Mine[] mines = message.mines;
        int squaredRadius = message.brushSize/ 2 * message.brushSize / 2;
        int startX = message.x - message.brushSize / 2;
        int startY = message.y - message.brushSize / 2;
       
            for (int x = startX; x < startX+ message.brushSize; x++)
            {
                for (int y = startY; y < startY+message.brushSize; y++)
                {
                    int u = x - message.x;
                    int v = y - message.y;

                
                     if (u * u + v * v < squaredRadius)
                     {
                            foreach (Mine mine in mines)
                            {
                                if ((int)mine.position.x == x && mine.position.y == y)
                                {
                                    Debug.Log("DEAD");
                                    endGame.SetActive(true);
                                }
                            }
                             try
                                {
                                    if (visitedMap[x, y] != 0)
                                    {
                                        filled++;
                                        float percent = filled / totalLength;
                                        percentFilled.text = percent.ToString();
                                        if (percent >= percentWin)
                                            {
                                                winGame.SetActive(true);
                                                return;
                                            }
                                        score += (int)(visitedMap[x, y] * 5);
                                        visitedMap[x, y] = 0;
                                        scoreText.text = score.ToString();
                                    }
                                }
                                catch(AndroidJavaException e)
                                {
                                    Debug.Log(e.Message);
                                }
                    }
                }
         
            }
    }
    //если вы дочитали до сюда, значит вам нечего делать, как и нам. Посмотрите на чудо код,
}
