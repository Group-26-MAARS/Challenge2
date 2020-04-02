using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Net;
using System.IO;



public class GetStatus : MonoBehaviour
{
    public String id;

    public void getStatus(int ID)
    {
        // get the ID of the current image target which is the same as the plant ID to use as the key in database
        // Make API call to get the date of last service for that plant ID
        // Get the curent month and day
        // compare current date and the date recived from api get the total number of days that have passed
        // make funcion call or set up enum to display proper status


        // get the local date and universal date
        // DateTime localDate = DateTime.Now;
        // DateTime utcDate = DateTime.UtcNow;

        // Debug.Log("This is the date" + localDate);
        // Debug.Log("This is the date utcDate" + utcDate);

       

    }

    // get the id to get the QR code
    public void setID(String ID)
    {
        id = ID;
    }

    private void displayStatus()
    {
        // nada right now.
    }

}
