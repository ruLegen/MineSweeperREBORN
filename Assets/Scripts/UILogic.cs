using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    // Start is called before the first frame update
    public Button menuButton;
    public Button brushSizeButton;
    public Button brushTypeButton;
    public Slider brushSizeSlider;
    public GameObject brushSizePanel;
    public GameObject brushIndicator;
    public GameObject drawerObject;

    private void Start()
    {
        menuButton.onClick.AddListener(menuButtonClick);
        brushSizeButton.onClick.AddListener(brushSizeButtonClicked);
        brushTypeButton.onClick.AddListener(menuButtonClick);
        brushSizeSlider.onValueChanged.AddListener(onSliderValueChanged);
        onSliderValueChanged(brushSizeSlider.value);
    }

    void onSliderValueChanged(float value)
    {
        float scale = Utilites.map(value,brushSizeSlider.minValue,brushSizeSlider.maxValue,0,1);
        brushIndicator.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
        drawerObject.GetComponent<Drawer>().brushRadius = (int)value;

    }
    void menuButtonClick()
    {
        brushSizePanel.SetActive(false);

    }
    void brushSizeButtonClicked()
    {
        brushSizePanel.SetActive(true);
    }
}
