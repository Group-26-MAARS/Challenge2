using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.Networking;

public class PlantAPICalls : MonoBehaviour
{
    public void get()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://maars-api.herokuapp.com/"));

    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

       // Request and wait for the desired page.
       yield return webRequest.SendWebRequest();

       if (webRequest.isNetworkError)
       {
                Debug.Log( "Error: " + webRequest.error);
       } else {
                Debug.Log("SUCCESS" + webRequest.downloadHandler.text);
       }
    }
    }



