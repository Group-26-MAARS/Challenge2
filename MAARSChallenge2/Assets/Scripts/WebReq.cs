using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebReq : MonoBehaviour
{

    // API Addresses 
    readonly string getAllURL = "https://maars-api.herokuapp.com/equipment";
    readonly string getWithIDURL = "https://maars-api.herokuapp.com/equipment/";
    readonly string postURL = "https://maars-api.herokuapp.com/equipment/new";
   

    // In game UI elements an
    public RawImage QRImage;
    public Text showNewQRDate;
    public Text showNewQRID;
    public Dropdown plantDD;
    public Dropdown allPlantsDD;

    // Should be taken care of better vars
    public DateTime currentDate = DateTime.Now;
    private string qrLink;
    private int newID;
    private int flag = 0;


    // Start is called before the first frame update
    // Gets all the plants in the DB
    void Start()
    {
        // When the Program Loads make the API request to get all the plants.
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
            Debug.Log("Request successful! All plants loaded");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            }

            // Get the Jason convert it to a string
            string rawJSON = www.downloadHandler.text.ToString();

            // create a new plant collection that holds the list of plants
            //PlantCollection plantList = new PlantCollection();

            // Make the list of plant objs fro the raw JSON
            PlantCollection.PlantList = JsonConvert.DeserializeObject<List<Plant>>(rawJSON);
            Debug.LogError(PlantCollection.PlantList.Count);

            // Calculat the proper status for each of the plants and set that field
            // for each plant obj.
            foreach (Plant p in PlantCollection.PlantList)
            {
                double numDays = (currentDate - p.dateOfLastService).TotalDays;

                if (numDays >= 9)
                {
                    p.Status = 4;  // Overdue

                }
                else if (numDays < 9 && numDays >= 5)
                {
                    p.Status = 3; // Due

                }
                else if (numDays < 5 && numDays >= 3)
                {
                    p.Status = 2; // Coming Due
                }
                else
                {
                    p.Status = 1; // All Good *Sunglasses Emoji*
                }

                // Note: This can (and should) be moved to another method.
                // Note: A field should be added to the DB to allow for more dynamic schedules. Allowing the user
                // to specify exact intervals for each status when adding the plant to the DB. (Will do this if there is time)
                // instead of having the hard coded values we could get the intervals from the plant objs
            }

            DropDown.PopulateDropdown(plantDD);
            DropDown.PopulateDropdownAll(allPlantsDD);
           

            if (flag == 1)
            {
                // get the most recent QR link and id to be displayed
                qrLink = PlantCollection.PlantList[PlantCollection.PlantList.Count - 1].qrCode.link;
                var id = PlantCollection.PlantList[PlantCollection.PlantList.Count - 1]._id;

                displayQR(qrLink, id);
            }
        }
    }

    // creating a new plant from genarated _id 
    public void onAddPlantPost()
    {
        // Get the newly generated id
        newID = PlantCollection.genNewID();
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

                flag = 1;
                // Update Plant List.
                Start();
            }
        }
    }

    // get specfic plant information
    public void GetWithID(int curID)
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

            //Plant obj = Plant.CreateFromJSON(www.downloadHandler.text);

            //Debug.Log(obj.daysOver);
            //Debug.Log(obj.dateOfLastService);

        }


    }

    // Show the OR on screen
    public void displayQR(String URL, double id)
    {
       StartCoroutine(loadSpriteImageFromUrl(URL, id));
    }

    IEnumerator loadSpriteImageFromUrl(string URL, double ID)
    {   
        // create image save name and type
        string QRID = newID + ".jpg";
        
        // get the QR from the URL
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.LogError("Texture succesfuly loaded");
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            
            // get the QR to display
            QRImage.texture = myTexture;

            // Show the id and Date to user
            showNewQRDate.text = currentDate.ToString();
            showNewQRID.text = newID.ToString();

            // Download QR to Photo Lib.
            byte[] bytes = myTexture.EncodeToJPG();
            NativeGallery.SaveImageToGallery(bytes, "Saved QR Codes", QRID);
        }
    }

    public void updateStatus()
    {
        int id = DropDown.updateFromSelection(plantDD);

    }

    public void deletePlant()
    {
        int id = DropDown.deleteFromSelection(allPlantsDD);   
    }
}

