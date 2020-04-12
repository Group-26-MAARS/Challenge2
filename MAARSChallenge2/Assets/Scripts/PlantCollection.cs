using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollection
{
    private static List<Plant> plantList;

    internal List<Plant> PlantList { get => plantList; set => plantList = value; }

    public void getStatus()
    {
        // Locate the Plant with the ID
        Debug.Log("Count " + plantList.Count);
    }

    public void createNewPlant(Plant tempReturn)
    {
        // gernerate ID make call to get webWithID
        // add it to the Vuforia Database
        // Add it to the current List
    }
}