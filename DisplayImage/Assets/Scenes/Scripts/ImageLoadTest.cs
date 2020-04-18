using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Net;

public class ImageLoadTest : MonoBehaviour
{

   public RawImage QR;

   public void onShowImageButton()
    {
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://i.imgur.com/nGLe10N.png");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            QR.texture = myTexture;

            byte[] bytes = myTexture.EncodeToJPG();

            NativeGallery.SaveImageToGallery(bytes, "Plants", "300.jpg");

            //var mediaScanIntent =
            //new Intent(Intent.ActionMediaScannerScanFile);
            //mediaScanIntent.SetData(_uri);
            //SendBroadcast(mediaScanIntent);
        }

       
    }
}
