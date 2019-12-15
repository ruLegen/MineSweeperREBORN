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
    Button btn;
    private void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    void onClick()
    {
        GameParams.initializeParams(mineCount, minPower, maxPower);
        StartCoroutine(LoadAsyncScene(lvl));
    }
    IEnumerator LoadAsyncScene(int lvl)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lvl);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (asyncLoad.progress == 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;

        }

    }
}
