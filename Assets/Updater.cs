using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Updater : MonoBehaviour
{
    public const string version = "a.0.0.1";

    
    void Awake()
    {
        if (CheckForUpdate()){
            // client.DownloadFile("https://github.com/iamsamhhh/UnityAutoUpdater/releases/download/" )
        }
    }

    bool CheckForUpdate(){
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/iamsamhhh/UnityAutoUpdater/releases");
        request.UserAgent = "something";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string htmlString;
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            htmlString = reader.ReadToEnd();
        }
        Debug.Log(htmlString);
        if (htmlString.Contains(version))
        return true;
        else
        return false;
    }

#if UNITY_EDITOR
    [MenuItem("AutoUpdater/Export configuration %e", false, 1)]
    private static void ExportConfiguration(){
        SaveMgr.instance.Save(version, "version");
    }
#endif 

    
}
