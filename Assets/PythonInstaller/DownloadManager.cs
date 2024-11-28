using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;

public class Download : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tuple<IEnumerator, string> result = DownloadTemp("https://upload.wikimedia.org/wikipedia/en/9/98/Tony_Hawk%27s_Underground_PlayStation2_box_art_cover.jpg");
        string path = result.Item2;
        Debug.Log(path);
    }

    // Some code imported from the Unity website for UnityWebRequest:
    // https://docs.unity3d.com/Manual/UnityWebRequest-CreatingDownloadHandlers.html
    public IEnumerator DownloadFile(string url, string path)
    {
        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        uwr.timeout = 10;
        uwr.downloadHandler = new DownloadHandlerFile(path, false);
        yield return uwr.SendWebRequest();

        Debug.Log(uwr.result);
        if (uwr.result != UnityWebRequest.Result.Success)
            Debug.LogWarning(uwr.error);
        else
            Debug.Log("File successfully downloaded and saved to " + path);
    }

    Tuple<IEnumerator, string> DownloadTemp (string url)
    {
        string randomFileName = (Guid.NewGuid().ToString()).Replace("-", "") + ".tmp.jpg";
        string tempPath = Path.Combine(Path.GetTempPath(), randomFileName);
        IEnumerator downloadedFile = DownloadFile(url, tempPath);
        return Tuple.Create(downloadedFile, tempPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
