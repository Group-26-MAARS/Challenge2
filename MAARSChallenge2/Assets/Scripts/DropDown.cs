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

    public static void PopulateDropdownAll(Dropdown plantDD)
    {
        List<string> options = new List<string>();
        string op;


        foreach (Plant p in PlantCollection.PlantList)
        {
           
          
                if (p.Status == 2)
                {
                    op = "ID: " + p._id + ", Status: Coming Due";

                }
                else if (p.Status == 3)
                {
                    op = "ID: " + p._id + ", Status: Due";
                }
                else if (p.Status == 4)
                {
                    op = "ID: " + p._id + ", Status: OverDue";
                } else
                {
                    op = "ID: " + p._id + ", Status: OK";
                }

                options.Add(op); // Or whatever you want for a label
           
        }
        plantDD.ClearOptions();
        plantDD.AddOptions(options);
    }

    public static int updateFromSelection(Dropdown DD)
    {
        string selection = DD.options[DD.value].text; 
        string trim = selection.Trim('I','D',':', ' ');
        string[] getZero = trim.Split(',');
        int idToUpdate = Int32.Parse(getZero[0]);
        DD.options.Remove(DD.options[DD.value]);

        foreach (Plant p in PlantCollection.PlantList)
        {
            if (p._id == idToUpdate)
            {
                LogInfo l = new LogInfo();
                l.addToLog("WATERED: Plant ID: " + p._id + ", Date of most recent servic: " + p.dateOfLastService + ", Status: OK" + "\n");
                p.Status = 1;

            }
        }

        return idToUpdate;
    }


    public static int deleteFromSelection(Dropdown DD)
    {
        string selection = DD.options[DD.value].text;
        string trim = selection.Trim('I', 'D', ':', ' ');
        string[] getZero = trim.Split(',');
        int idToUpdate = Int32.Parse(getZero[0]);
        DD.options.Remove(DD.options[DD.value]);

        foreach (Plant p in PlantCollection.PlantList)
        {
            if (p._id == idToUpdate)
            {
                LogInfo l = new LogInfo();
                l.addToLog("DELETED: Plant ID: " + p._id + ", Date of most recent servic: " + p.dateOfLastService + "\n");

                p.Status = 0;
            }
        }

        return idToUpdate;
    }
}
