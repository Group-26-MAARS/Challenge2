using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusManager 
{
    public static void Status(int statusIndicator)
    {

        switch (statusIndicator)
        {
            case 1:
                Debug.LogError("All Good");
                break;
            case 2:
                Debug.LogError("Coming Due");
                break;
            case 3:
                Debug.LogError("Due");
                break;
            case 4:
                Debug.LogError("Overdue");
                break;
            default:
                Debug.LogError("Default case");
                break;
        }
    }
}
