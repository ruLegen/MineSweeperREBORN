using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Drawer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IDragHandler
{
    // Start is called before the first frame update
    public Texture2D backgroundOriginal;
    Texture2D background;
    public int brushRadius = 5;
    bool isDrawing = false;

    Image img;
    void Start()
    {
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

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = eventData.position;
        float xRation = background.width * 1.0f / Screen.width;
        float yRation = background.height * 1.0f / Screen.height;
        //Debug.Log(Mathf.Ceil(position.x * xRation));
        //Debug.Log(Mathf.Ceil(position.y * yRation));
        int xOnTexture = (int)Mathf.Ceil((position.x  * xRation) - brushRadius / 2);
        int yOnTexture = (int)Mathf.Ceil((position.y  * yRation) - brushRadius / 2);

        var pixels = background.GetPixels(xOnTexture, yOnTexture, brushRadius, brushRadius, 0);

        for(int x = 0; x<  pixels.Length;x++)
        {
            pixels[x] =new Color(pixels[x].r, pixels[x].g, pixels[x].b, 0);
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
