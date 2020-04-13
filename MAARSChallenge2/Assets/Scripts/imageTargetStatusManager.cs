using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class imageTargetStatusManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getStatus()
    {
        PlantCollection pl = new PlantCollection();
        List<Plant> plantList = pl.PlantList;

        foreach (Plant p in plantList)
        {
            Debug.LogError(p.Status);
        }

    }
}
