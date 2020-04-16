using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlantCollection 
{
    private static List<Plant> plantList;
    public static List<int> IDs;
    public static int currentID;
    public static String qrImageURL;

    internal static List<Plant> PlantList { get => plantList; set => plantList = value; }
  
    // Generate new plant ID
    public static int genNewID ()
    {
        createIDList();
        System.Random rnd = new System.Random();
        int newID = rnd.Next(10000);
        int count = 0;

        // making sure the QR ID is not in use and randomly generating if it is
        // will try up to 2000 random numbers.
        while (IDs.Contains(newID))
        {
            newID = rnd.Next(10000);

            if (count == 2000)
            {
                Debug.LogError("Unique ID could not be generated after 2000 attempts.");
                break;
            }   
        }

        // add the new ID to the ID list
        IDs.Add(newID);
        currentID = newID;
        return newID;
    }

    // Create a list of all the current IDs so that we make sure the ID is unique
    private static void createIDList()
    {
        PlantCollection.IDs = new List<int>();
        foreach (Plant p in plantList)
        {
            IDs.Add((int)p._id);
        }
    }
    
    // get the url from the plant thats been created
    public static string urlToDisplay ()
    {
        foreach (Plant p in plantList)
        {
            if (p._id == currentID)
            {
                qrImageURL = p.qrCode.link;
            }
        }
        return qrImageURL;
    }
   


}