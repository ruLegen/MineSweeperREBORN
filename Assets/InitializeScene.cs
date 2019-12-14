using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GenerateHeatMap heatMapGenerator;
    private void Awake()
    {
        heatMapGenerator.maxPower = GameParams.maxPower;
        heatMapGenerator.minPower = GameParams.minPower;
        heatMapGenerator.mineCount = GameParams.mineCount;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
