using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlantCollection
{
    private static List<Plant> plantList;

    internal static List<Plant> PlantList { get => plantList; set => plantList = value; }

    public static void getStatus()
    {
        // Locate the Plant with the ID
        Debug.Log("Count " + plantList.Count);
    }

    public static void createNewPlant(Plant tempReturn)
    {
        // gernerate ID make call to get webWithID
        // add it to the Vuforia Database
        // Add it to the current List
    }
}