using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InitializeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GenerateHeatMap heatMapGenerator;
    public GameObject preloader;
    private void Awake()
    {
        heatMapGenerator.maxPower = GameParams.maxPower;
        heatMapGenerator.minPower = GameParams.minPower;
        heatMapGenerator.mineCount = GameParams.mineCount;
        preloader.SetActive(true);

        StartCoroutine(GenerateHeatMap());
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
