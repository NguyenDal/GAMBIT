using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.IO;


public class WebGlFileLoader : MonoBehaviour
{
    public static IEnumerator LoadFile(string fileName, Action<string> onSuccess, Action<string> onError)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        using (UnityWebRequest request = UnityWebRequest.Get(filePath))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }
}
