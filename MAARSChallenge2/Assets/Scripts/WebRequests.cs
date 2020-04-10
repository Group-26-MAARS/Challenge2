using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

// It seems we will need to creat a list of all the plants in the Database
// get all the plants 
// parse into individule plants
// save the status
// when that one is seen show the status

public class WebRequests : MonoBehaviour
{
    // API Addresses
    readonly string getAllURL = "https://maars-api.herokuapp.com/equipment";
    readonly string getWithIDURL = "https://maars-api.herokuapp.com/equipment/";
    readonly string postURL = "https://maars-api.herokuapp.com/equipment/new";

    // the type of the current id will be determined by Vuforia
    private int curID;
    // The type must be an int to create a new plant
    private int newID = -9000;
    public List<string> trackList = new List<string>(); 
   

    // the type of the current id will be determined by Vuforia
    public void getID()
    {
            // curID = Int32.Parse(tb.TrackableName);
    }

 // ---------------------------------------------------------------------------------
    // Gets all the plants in the Data base
    public void onButtonGet()
    {
        StartCoroutine(getRequest(getAllURL));
    }

    IEnumerator getRequest(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Got the Get");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            }

            // Print Headers
            // Debug.Log(sb.ToString());

            // Print Body
            Debug.Log(www.downloadHandler.text);
        }
    }
// ----------------------------------------------------------------------------------
    // creating a new plant from genarated _id 
    public void onButtonPost()
    {
        StartCoroutine(newReqest(postURL, newID));
    }

    IEnumerator newReqest(string url, int data)
    {
        WWWForm form = new WWWForm();
        form.AddField("_id", data);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("There was an error with post " + www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    //-------------------------------------------------------------------------------

    // get specfic plant information
    public void onButtonGetWithID()
    {
        StartCoroutine(getSpecficPlant(getWithIDURL, curID));
    }

    IEnumerator getSpecficPlant(string url, int ID)
    {

        // appaned url and ID
        string strID = ID.ToString();
        string appendedString = url + strID;
        Debug.Log(appendedString);

        UnityWebRequest www = UnityWebRequest.Get(appendedString);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("There was an error with getting specfic plant " + www.error);
        }
        else
        {
            Debug.Log("successfully got plant info");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            }


            Plant obj = Plant.CreateFromJSON(www.downloadHandler.text);

            double daysElapsed = obj.getDaysElapsed();
            obj.showStatus(daysElapsed);
        }
    }
}
// --------------------------------------------------------------------------------
[Serializable]
public class Plant
{
    // Another option DateTime utcDate = DateTime.UtcNow;
    public DateTime currentDate = DateTime.Now;
    private double daysElapsed;
    public int daysOver;
    public int _id;
    public string dateOfLastService;
    // we will deal with qr list later


    public static Plant CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Plant>(jsonString);
    }

    public double getDaysElapsed()
    {
        string[] words = dateOfLastService.Split('-', 'T');
        int year = Int32.Parse(words[0]);
        int month = Int32.Parse(words[1]);
        int day = Int32.Parse(words[2]);

        DateTime saveDate = new DateTime(year, month, day);
        return (currentDate - saveDate).TotalDays;
    }

    public void showStatus(double daysElapsed)
    {
      
        if (daysElapsed > 5)
        {
            // show red
            Debug.Log("OverDue");

        }
        else if (daysElapsed <= 5 && daysElapsed > 3)
        {
            // show orange
            Debug.Log("Due");
        }
        else if (daysElapsed <= 3 && daysElapsed > 2)
        {
            // show yellow
            Debug.Log("ComingDue");
        }
        else
        {
            // show green
            Debug.Log("A okay");
        }
    }
}

// -------------------------------------------------------------------------------------------------
class PlantDictionary
{
    public static Dictionary<int, Plant> plantList = new Dictionary<int, Plant>();

    // if the plant hasnt been seen yet add it to the list.
    public void addPlantToDictionary(int ID, Plant newplant)
    {
        plantList.Add(ID, newplant);
    } 

    // return the plant with that key
    public Plant getPlantFromDictionary(int ID)
    {
        return plantList[ID];
    }

    // does the plant list contain the current id?
    public bool containsPlant(int ID)
    {
        return plantList.ContainsKey(ID);
    }
}
