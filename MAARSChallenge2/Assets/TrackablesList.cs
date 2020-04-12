using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackablesList : MonoBehaviour
{
 

    public List<int> trackList = new List<int>();
    // Update is called once per frame
    void Update()
    {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        Debug.Log("List of trackables currently active (tracked): ");
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            if (!trackList.Contains(Int32.Parse(tb.TrackableName)))
            {
                trackList.Add(Int32.Parse(tb.TrackableName));
            }
            Debug.Log("Trackable: " + tb.TrackableName);
        }
    }

    public void printTrackList()
    {
        foreach(int id in trackList)
        {
            Debug.LogError("THis " + id);
        }
    }

    public void createPlantsList()
    {
        foreach(int ID in trackList)
        {
            //PlantList.addPlantToDictionary()
        }
    }
}
