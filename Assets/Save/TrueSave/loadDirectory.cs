using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loadDirectory : MonoBehaviour
{
    public TextMeshProUGUI steamPathText;
    public TextMeshProUGUI documentPathText;

    void Start()
    {
        idGenerator._idGenerator.load();
        if(string.IsNullOrEmpty(idGenerator._idGenerator.getDocumentsPath()) == true)
        {
            Debug.LogWarning("File path for documents " + idGenerator._idGenerator.getDocumentsPath() + " is invalid");
        }
        if (string.IsNullOrEmpty(idGenerator._idGenerator.getSteamPath()) == true)
        {
            Debug.LogWarning("File path for documents " + idGenerator._idGenerator.getSteamPath() + " is invalid");
        }

        steamPathText.text += " " + idGenerator._idGenerator.getSteamPath();
        documentPathText.text += " " + idGenerator._idGenerator.getDocumentsPath();


    }
}
