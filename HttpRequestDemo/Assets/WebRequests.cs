using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequests : MonoBehaviour
{
    readonly string getAllURL = "https://maars-api.herokuapp.com/equipment";
    readonly string getWithIDURL = "https://maars-api.herokuapp.com/equipment/new/:id/";
    readonly string postURL = "https://maars-api.herokuapp.com/equipment/new";

    private string curID;
    private string newID = "fk454";

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
        }
    }

    public void onButtonPost()
    {
        StartCoroutine(newReqest(postURL, newID));
    }

    IEnumerator newReqest(string url, string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("_id", data );

        using (UnityWebRequest www = UnityWebRequest.Post("url", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

}
