using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrawEvent : UnityEvent<OnDrawMessage> { }

public class Drawer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IDragHandler
{
    // Start is called before the first frame update
    public static DrawEvent onDrawEvent = new DrawEvent();

    public Texture2D backgroundOriginal;
    Texture2D background;
    public int brushRadius = 5;
    int squaredRadius;
    bool isDrawing = false;
    public GameObject generatedMap;
    Image img;
    void Start()
    {
        squaredRadius = brushRadius/2 * brushRadius/2;
        img = gameObject.GetComponent<Image>();
        Debug.Log("Drag me!");
        background = CopyTexture(backgroundOriginal);

        img.sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0, 0));
    }

   
    // Update is called once per frame

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        isDrawing = true;
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = eventData.position;
        squaredRadius = brushRadius / 2 * brushRadius / 2;
        int offsetX = Screen.width- (int)gameObject.GetComponent<RectTransform>().rect.width;
        int offsetY = Screen.height-(int)gameObject.GetComponent<RectTransform>().rect.height;


      
        float xRation = background.width * 1.0f / (Screen.width - offsetX);
        float yRation = background.height * 1.0f / (Screen.height - offsetY);


        int xOnTexture = (int)Mathf.Ceil(((position.x- offsetX) * xRation) - brushRadius / 2);
        int yOnTexture = (int)Mathf.Ceil((position.y * yRation) - brushRadius / 2);

        GenerateHeatMap generateHeatMap = generatedMap.GetComponent<GenerateHeatMap>();

        //float xMapTextureRatio = generateHeatMap.width / background.width;
        //float yMapTextureRatio = generateHeatMap.width / 1.77777778f / background.height;

        //int xOnMap = (int)(xOnTexture * xMapTextureRatio);
        //int yOnMap = (int)(yOnTexture * yMapTextureRatio);

        var pixels = background.GetPixels(xOnTexture, yOnTexture, brushRadius, brushRadius, 0);
        try
        {
           // Debug.Log(xOnMap.ToString()+"  "+ yOnMap.ToString());
            onDrawEvent.Invoke(new OnDrawMessage((int)position.x,(int) position.y, (int)(brushRadius), generateHeatMap.mines,generateHeatMap.normilized));
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }

        for (int x = 0; x <brushRadius; x++)
        {
            for (int y = 0; y < brushRadius; y++)
            {
                //пому что он одномерный
                int index = (x * brushRadius) + y;
                int u = x - brushRadius / 2;
                int v = y - brushRadius / 2;
                if (u*u + v*v < squaredRadius)
                    pixels[index] = new Color(pixels[index].r, pixels[index].g, pixels[index].b, 0);
            }

        }
        background.SetPixels(xOnTexture, yOnTexture, brushRadius, brushRadius, pixels);
        background.Apply();
    }


    public Texture2D CopyTexture(Texture2D src)
    {
        Texture2D newTexture = new Texture2D(src.width, src.height);
        var pixels = src.GetPixels(0, 0, src.width, src.height);
        var newPixels = new Color[pixels.Length];

        for (int i = 0; i < newPixels.Length; i++)
        {
            Color clr = new Color(pixels[i].r, pixels[i].r, pixels[i].b, pixels[i].a);
            newPixels[i] = clr;
        }

        newTexture.SetPixels(newPixels);
        newTexture.Apply();

        return newTexture;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag me!");

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        isDrawing = false;

    }
}
