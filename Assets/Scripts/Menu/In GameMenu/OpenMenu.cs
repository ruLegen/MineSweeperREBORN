using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject closeObject;
    Button btn;
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(closeMenu);
    }
    void closeMenu()
    {
        closeObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
