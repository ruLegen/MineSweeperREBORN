using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InitializeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GenerateHeatMap heatMapGenerator;
    public GameObject preloader;
    public GameObject EndMenu;
    public Text text;
    public AudioSource audioSource;
    public float totalSeconds = 300;

    public float time;
    private void Awake()
    {
        heatMapGenerator.maxPower = GameParams.maxPower;
        heatMapGenerator.minPower = GameParams.minPower;
        heatMapGenerator.mineCount = GameParams.mineCount;
        preloader.SetActive(true);

        StartCoroutine(GenerateHeatMap());
    }
    void Start()
    {
        time = Time.time;
        GameLogic.isEndGame = false;
        audioSource = gameObject.GetComponent<AudioSource>();

    }


private void FixedUpdate()
    {

        if (!GameLogic.isEndGame )
        {
            float timeLeft = totalSeconds - (Time.time - time);
            text.text = timeLeft.ToString("0.0");
            if (timeLeft <= 0)
            {
                GameLogic.isEndGame = true;
                EndMenu.SetActive(true);
            }
        }
        else
        {
            audioSource.Stop();
        }

    }
    IEnumerator GenerateHeatMap()
    {

        while (!heatMapGenerator.isDone)
        {
            yield return null;
            heatMapGenerator.Generate();

        }
        preloader.SetActive(false);

    }
   
}
