using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenerateHeatMap : MonoBehaviour
{
    // Start is called before the first frame update

    Image img;
    Sprite spr;
    Texture2D texture;
    public int maxPower = 1000000;
    public int minPower = 1000000;
    public int mineCount = 6;

    //normalized array of map;
    public float[,] normilized;
    public int width = 2160;
    public int height = 1080;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public Mine[] mines;
    void Start()
    {

        img = gameObject.GetComponent<Image>();
        int min;
        int max;
        width = Screen.width;
        height = Screen.height; ;
        //height = (int)(width / 1.7777777778); // 1.7 = 16/9
        gradient = new Gradient();
        colorKey = new GradientColorKey[5];
        colorKey[0].color = Color.red;
        colorKey[0].time = 1f;

        colorKey[1].color = Color.yellow;
        colorKey[1].time = 0.75f;

        colorKey[2].color = Color.green;
        colorKey[2].time = 0.50f;

        colorKey[3].color = new Color(0.05f, 0.36f, 0.65f);
        colorKey[3].time = 0.35f;

        colorKey[4].color = Color.blue;
        colorKey[4].time = 0f;



        alphaKey = new GradientAlphaKey[5];
        for (int i = 0; i < 5; i++)
        {
            alphaKey[i].alpha = 1.0f;
            alphaKey[i].time = 0.0f;
        }


        gradient.SetKeys(colorKey, alphaKey);

        Texture2D txt = new Texture2D(width, height);
        mines = Utilites.generateMines(width, height, mineCount, minPower, maxPower);
        var matrix = Utilites.getMatrix(width, height, mines, out min, out max);
        normilized = Utilites.normolizeMatrix(matrix, width, height, max, min);
        for (int x = 0; x < txt.width; x++)
        {
            for (int y = 0; y < txt.height; y++)
            {
                txt.SetPixel(x, y, gradient.Evaluate(normilized[x, y]));
            }
        }

        txt.Apply();
        Sprite spr = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0, 0));
        img.sprite = spr;



    }

}
   
