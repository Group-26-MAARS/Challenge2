using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequests : MonoBehaviour
{
    // API Addresses
    // Could simplify into one and append in methods 
    readonly string getAllURL = "https://maars-api.herokuapp.com/equipment";
    readonly string getWithIDURL = "https://maars-api.herokuapp.com/equipment/";
    readonly string postURL = "https://maars-api.herokuapp.com/equipment/new";

    // the type of the current id will be determined by Vuforia
    private int curID = -9000;

    // The type must be an int to create a new plant
    private int newID = -9000;

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

            // Print Headers
            // Debug.Log(sb.ToString());

            // Print Body
            //Debug.Log(www.downloadHandler.text);

            Plant obj = Plant.CreateFromJSON(www.downloadHandler.text);

            Debug.Log(obj.daysOver);
            Debug.Log(obj.dateOfLastService);
        
        }


    }

}

[Serializable]
class Plant
{
    public int daysOver;
    public int _id;
    public string dateOfLastService;
    // we will deal with qr list later


    public static Plant CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Plant>(jsonString);
    }


}
