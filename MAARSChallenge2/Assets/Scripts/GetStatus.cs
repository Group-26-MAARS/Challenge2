using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Net;
using System.IO;



public class GetStatus : MonoBehaviour
{
    // get the local date and universal date
    private DateTime curDate = DateTime.Now;
    // Another option DateTime utcDate = DateTime.UtcNow;

    private DateTime plantSaveDate;
    private double daysElapsed;

    // Debug.Log("This is the date" + localDate);

    public void getStatus(int ID)
    {
        // test with specfic year,month,day
        plantSaveDate = new DateTime(2020, 4, 3);

        // calculate the number of days elapsed
        daysElapsed = getDaysElapsed(curDate, plantSaveDate);

        // show the status of the plant
        showStatus(daysElapsed);

    }
    
    // create a method that returns a plant object and in that plant obj we can get the id and 
    // date.

    // calculate how many days have passed
    private double getDaysElapsed(DateTime curDate, DateTime saveDate)
    {
        return (curDate - saveDate).TotalDays;
    }

    private void showStatus(double daysElapsed)
    {
        if (daysElapsed > 5)
        {
            // show red
            Debug.Log("OverDue");

        } else if (daysElapsed <= 5 && daysElapsed > 3) 
        {
            // show orange
            Debug.Log("Due");
        } else if (daysElapsed <= 3 && daysElapsed > 2)
        {
            // show yellow
            Debug.Log("ComingDue");
        } else
        {
            // show green
            Debug.Log("A okay");
        }
    }


}
