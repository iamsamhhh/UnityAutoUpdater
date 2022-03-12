using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;




public class Updater : MonoBehaviour
{
    public const string version = "a.0.0.1";
    Response root;
    void Awake()
    {
        GetWebResponse();
        if (root.Name != version)
        {
            WebClient client = new WebClient();
            client.DownloadFile("https://github.com/iamsamhhh/UnityAutoUpdater/releases/download/" + root.Name + "/AutoUpdater_Mac_v1.app.zip", "AutoUpdater_Mac_v1.app.zip");
        }
       
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
