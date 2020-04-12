using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void printCount()
    {
        PlantCollection pl = new PlantCollection();
        List<Plant> plantList = pl.PlantList;
        
        foreach (Plant p in plantList)
        {
            Debug.LogError(p._id);
        }
    }
}
