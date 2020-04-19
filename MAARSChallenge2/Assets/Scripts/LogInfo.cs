using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogInfo : MonoBehaviour
{
   
    public Text logText;
    // Start is called before the first frame update
    void Start()
    {

       string path = Application.persistentDataPath + "/Log.txt";

        if(!File.Exists(path))
        {
            File.WriteAllText(path, "Activity Log \n");
        }

        //File.WriteAllText(path, "Activity Log \n");
        string timeOfUse = "LOGIN DATE: " + System.DateTime.Now + "\n";
        File.AppendAllText(path, timeOfUse);
    }

    public void addToLog(string text)
    {
        string path = Application.persistentDataPath  + "/Log.txt";
        File.AppendAllText(path, text);
    }

    public void showLog()
    {
        string path = Application.persistentDataPath + "/Log.txt";
        logText.text = File.ReadAllText(path);

    }

    public void clearLogs()
    {
        logText.text = "Activity Log";
        string path = Application.persistentDataPath + "/Log.txt";
        File.WriteAllText(path, "Activity Log \n");
        logText.text = File.ReadAllText(path);
    }


}