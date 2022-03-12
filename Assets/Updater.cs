using System.Net;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System.Security.Permissions;
using System.ComponentModel;
using System;

public class Updater : MonoBehaviour
{
    public const string version = "a.0.0.1";
    Response root;
    [SerializeField]
    TMP_Text text;
    void Awake()
    {
        text.text = version;
    }

    public void UpdateVersion()
    {
        GetWebResponse();
        if (root.Name != version)
        {
            Debug.Log("start downloading update");
            text.text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadFileCompleted += OnDownloadComplete;
            client.DownloadFileTaskAsync("https://github.com/iamsamhhh/UnityAutoUpdater/releases/download/" + root.Name + "/AutoUpdater_Mac.app.zip", "AutoUpdater_Mac.app.zip");
        }
    }

    void OnDownloadComplete(object obj, AsyncCompletedEventArgs a) {
        Debug.Log("download completed");
        text.text = version;
        if (File.Exists(Application.dataPath + "/../../AutoUpdater_Mac.app.zip"))
        {
            Debug.Log("moving file...");
            //File.Move(Application.dataPath + "/../AutoUpdater_Mac.app.zip", Application.dataPath + "/../../AutoUpdater_Mac.app.zip");
            System.IO.Compression.ZipFile.ExtractToDirectory(Application.dataPath + "/../../AutoUpdater_Mac.app.zip",
                Application.dataPath + "/../../AutoUpdater");
            File.Delete(Application.dataPath + "/../../AutoUpdater_Mac.app.zip");
            //File.Move(Application.dataPath + "/../../AutoUpdater/AutoUpdater.app", Application.dataPath + "/../../AutoUpdater.aoo");

            //File.Open(Application.dataPath + "/../../AutoUpdater/AutoUpdater.app", FileMode.Open);
        }
    }

    bool locker = false;
    private void Update()
    {
        //if (File.Exists(Application.dataPath + "/../../AutoUpdater/AutoUpdater.app") && !locker) {
        //    File.Move(Application.dataPath + "/../../AutoUpdater/AutoUpdater.app", Application.dataPath + "/../../AutoUpdater.app");
        //    Debug.Log("move successed");
        //    locker = true;
        //}
    }

    void GetWebResponse()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/iamsamhhh/UnityAutoUpdater/releases/latest");
        request.UserAgent = "whyyyyyyyyy"; // Don't know why but it only works when this line exist
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string htmlString;
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            htmlString = reader.ReadToEnd();
        }
        root = JsonConvert.DeserializeObject<Response>(htmlString);
    }

}
