using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class idGenerator : MonoBehaviour
{
    public string savePath => $"{Application.persistentDataPath}/idGenerator.lemon";
    public static idGenerator _idGenerator;

    //Counter
    counterPackage currentCounter;

    private void Start()
    {
        _idGenerator = this;
        currentCounter = new counterPackage();
        currentCounter.currentCounter = -1;
        currentCounter.steamPath = "";
        currentCounter.documentPath = "";
        load();
        //Debug.Log(DEBUG_EntireDebug.DEBUG_entireDebug);
        DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isID_GEN, DEBUG_mode.ID_GEN, this.name);
    }
    public int getCurrentID()
    {
        return currentCounter.currentCounter;
    }
    public System.Guid generateNewID()
    {
        //currentCounter.currentCounter++;
        return System.Guid.NewGuid();
       // return currentCounter.currentCounter;
    }
    public void setSteamPath(string steamPath)
    {
        currentCounter.steamPath = steamPath;
    }
    public void setDocumentsPath(string documentsPath)
    {
        currentCounter.documentPath = documentsPath;
    }
    public string getSteamPath()
    {
        return currentCounter.steamPath;
    }
    public string getDocumentsPath()
    {
        return currentCounter.documentPath;
    }
    [ContextMenu("Reset Counter")]
    public void resetCounter()
    {
        currentCounter = new counterPackage();
        currentCounter.currentCounter = -1;
    }

    public void save()
    {
        using (FileStream stream = File.Open(savePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, currentCounter);
            stream.Close();
        }
    }
    public void load()
    {
        if (File.Exists(savePath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, savePath);
            save();
        }

        using (FileStream stream = File.Open(savePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            currentCounter = (counterPackage)formatter.Deserialize(stream);
        }
    }

    private void OnApplicationQuit()
    {
        save();
    }
}

[Serializable]
public struct counterPackage
{
    public int currentCounter;
    public string steamPath;
    public string documentPath;
}
