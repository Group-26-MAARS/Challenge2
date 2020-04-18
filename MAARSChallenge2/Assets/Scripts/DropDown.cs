using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class DropDown 
{

    public static void PopulateDropdown(Dropdown plantDD)
    {
        List<string> options = new List<string>();
        string op;
       

        foreach (Plant p in PlantCollection.PlantList)
        {
            if (p.Status >= 2) 
            {
                if (p.Status == 2) 
                {
                    op = "ID: " + p._id + ", Status: Coming Due";

                } else if(p.Status == 3)
                {
                    op = "ID: " + p._id + ", Status: Due";
                } else
                {
                    op = "ID: " + p._id + ", Status: OverDue";
                }
                
                options.Add(op); // Or whatever you want for a label
            }
        }
        plantDD.ClearOptions();
        plantDD.AddOptions(options);
    }

    public static void updateFromSelection(Dropdown DD)
    {
        string selection = DD.options[DD.value].text;
        Debug.LogError(selection);
        string trim = selection.Trim('I','D',':', ' ');
        Debug.LogError(trim);
        string[] getZero = trim.Split(',');
        Debug.LogError(getZero[0]);
        int idToUpdate = Int32.Parse(getZero[0]);
        Debug.LogError(idToUpdate);
    }
}
