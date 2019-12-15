using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    int score = 0;
    public static bool isEndGame = false;
    public GameObject endGame;
    public GameObject winGame;
    public GameObject canvas;
    public Text percentFilled;
    public GameObject DrawObject;
    public GameObject GameSoundeObject;

    int filled = 0;
    int totalLength = 0;
    public Text scoreText;
    public float percentWin = 70;
    private float[,] visitedMap = null;
    public GameObject ExplosionPrefab;
    public GameObject HolePrefab;

    void Start()
    {
        Drawer.onDrawEvent.AddListener(onDraw);
    }

    IEnumerator drawBombs(Mine[] mines)
    {
        foreach (Mine _mine in mines)
        {
            GameObject go = Instantiate(ExplosionPrefab);
            GameObject hole = Instantiate(HolePrefab);
            
            go.GetComponent<RectTransform>().SetParent(canvas.transform);
            hole.GetComponent<RectTransform>().SetParent(canvas.transform);
            float scale = Random.Range(150, 450);//Utilites.map((int)(_mine.force), GameParams.minPower - 1, GameParams.maxPower, 10.0f, 300);
            float holeScale = Utilites.map((int)(_mine.force), GameParams.minPower - 1, GameParams.maxPower, 0.3f, 3);

            go.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
            go.GetComponent<RectTransform>().localPosition = new Vector3(_mine.position.x, _mine.position.y, 0);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector3(_mine.position.x, _mine.position.y, 0);
            go.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, Random.Range(0, 359));

            hole.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
            hole.GetComponent<RectTransform>().localScale = new Vector3(holeScale, holeScale, holeScale);
            hole.GetComponent<RectTransform>().localPosition = new Vector3(_mine.position.x, _mine.position.y, 0);
            hole.GetComponent<RectTransform>().anchoredPosition = new Vector3(_mine.position.x, _mine.position.y, 0);
            Drawer drawer = DrawObject.GetComponent<Drawer>();
            drawer.DrawOn(_mine.position.x, _mine.position.y);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.6f));
        }
        endGame.SetActive(true);

    }

    void onDraw(OnDrawMessage message)
    {
        if(!isEndGame)
        {
            if (visitedMap == null)
            {
                visitedMap = message.normilized;
                totalLength = visitedMap.GetLength(0) + visitedMap.GetLength(1);
            }
            Mine[] mines = message.mines;
            int squaredRadius = message.brushSize / 2 * message.brushSize / 2;
            int startX = message.x - message.brushSize / 2;
            int startY = message.y - message.brushSize / 2;

            for (int x = startX; x < startX + message.brushSize; x++)
            {
                for (int y = startY; y < startY + message.brushSize; y++)
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
                                isEndGame = true;
                                GameObject go = Instantiate(ExplosionPrefab);
                                GameObject hole = Instantiate(HolePrefab);

                                go.GetComponent<RectTransform>().SetParent(canvas.transform);
                                hole.GetComponent<RectTransform>().SetParent(canvas.transform);
                                float scale = Random.Range(150, 450);//Utilites.map((int)(_mine.force), GameParams.minPower - 1, GameParams.maxPower, 10.0f, 300);
                                float holeScale = Utilites.map((int)(mine.force), GameParams.minPower - 1, GameParams.maxPower, 0.3f, 3);
                                go.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
                                go.GetComponent<RectTransform>().localPosition = new Vector3(mine.position.x, mine.position.y, 0);
                                go.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, Random.Range(0, 359));

                                go.GetComponent<RectTransform>().anchoredPosition = new Vector3(mine.position.x, mine.position.y, 0);
                                hole.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
                                hole.GetComponent<RectTransform>().localScale = new Vector3(holeScale, holeScale, holeScale);
                                hole.GetComponent<RectTransform>().localPosition = new Vector3(mine.position.x, mine.position.y, 0);
                                hole.GetComponent<RectTransform>().anchoredPosition = new Vector3(mine.position.x, mine.position.y, 0);
                                
                                StartCoroutine(drawBombs(mines));
                                endGame.SetActive(true);

                                break;
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
                                    GameSoundeObject.GetComponent<AudioSource>().Stop();
                                    return;
                                }
                                score += (int)(visitedMap[x, y] * 5);
                                visitedMap[x, y] = 0;
                                scoreText.text = score.ToString();
                            }
                        }
                        catch (AndroidJavaException e)
                        {
                            Debug.Log(e.Message);
                        }
                    }
                }

            }
        }
        
    }
    //если вы дочитали до сюда, значит вам нечего делать, как и нам. Посмотрите на чудо код,
}
