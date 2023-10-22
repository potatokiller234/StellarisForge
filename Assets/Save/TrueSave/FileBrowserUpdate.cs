
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileBrowserUpdate : MonoBehaviour
{

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        

        //new FileBrowser().OpenFileBrowser(bp, path =>
        //{
        //         StartCoroutine(LoadImage(path));
        //});
    }

    IEnumerator LoadImage(string path)
    {
        //Path is file player selected
        yield return new WaitForEndOfFrame();
    }
}
