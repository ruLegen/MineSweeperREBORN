using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update

    public int mineCount = 4;
    public int minPower = 100;
    public int maxPower = 100;
    public int lvl = 0;

    Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(changeScene);
    }

    void changeScene()
    {
        GameParams.initializeParams(mineCount, minPower, maxPower);
        SceneManager.LoadScene(lvl);
    }
}
