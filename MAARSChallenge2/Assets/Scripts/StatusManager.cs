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
                Debug.LogError("Case 1");
                break;
            case 2:
                Debug.LogError("Case 2");
                break;
            case 3:
                Debug.LogError("Case 3");
                break;
            case 4:
                Debug.LogError("Case 4");
                break;
            default:
                Debug.LogError("Default case");
                break;
        }
    }
}
