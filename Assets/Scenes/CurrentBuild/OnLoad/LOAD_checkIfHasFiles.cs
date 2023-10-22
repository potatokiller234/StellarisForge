using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LOAD_checkIfHasFiles : MonoBehaviour
{
    string mainDataPath;

    void Start()
    {
        LOAD_checkSaveAndSecret();
        LOAD_checkCustomFolder();
        LOAD_checkDebugFolder();
    }
    /// <summary>
    /// Checks to see if the Saves and Secret folders are there, if not then it will create it
    /// </summary>
    private void LOAD_checkSaveAndSecret()
    {
        mainDataPath = Application.persistentDataPath + "/Saves";

        if (Directory.Exists(mainDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, mainDataPath);
            Directory.CreateDirectory(mainDataPath);
        }
        mainDataPath += "/Secret";
        if (Directory.Exists(mainDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, mainDataPath);
            Directory.CreateDirectory(mainDataPath);
        }
        mainDataPath += "/Secret.txt";
        if (File.Exists(mainDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE, mainDataPath);
            StreamWriter sw = new StreamWriter(mainDataPath);
            sw.Write("https://www.youtube.com/watch?v=X_8Nh5XfRw0");
            sw.Close();
        }
        // OpenProjectView();
        mainDataPath = Application.persistentDataPath + "/Saves";
    }

    /// <summary>
    /// Checks to see if Application.persistentDataPath has the Random_Initalizer, if not then it will create it
    /// </summary>
    /// <returns></returns>
    public string LOAD_checkandWriteHardFiles()
    {
        string currentDataPath = Application.persistentDataPath + "/Default Initializers";
        if (Directory.Exists(currentDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, currentDataPath);
            Directory.CreateDirectory(currentDataPath);
        }
        currentDataPath += "/common";
        if (Directory.Exists(currentDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, currentDataPath);
            Directory.CreateDirectory(currentDataPath);
        }
        currentDataPath += "/solar_system_initializers";
        if (Directory.Exists(currentDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, currentDataPath);
            Directory.CreateDirectory(currentDataPath);
        }

        //Now we will check if random is there
        string nowDataPath = currentDataPath + "/Random.txt";
        if (File.Exists(nowDataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE, nowDataPath);
            StreamWriter bob = new StreamWriter(nowDataPath);
            bob.WriteLine("Random_Initializer = {");
            bob.WriteLine("   name = \"Example System\"");
            bob.WriteLine("}");
            bob.Close();
        }

        currentDataPath = Application.persistentDataPath + "/Default Initializers";
        currentDataPath = currentDataPath.Replace("/", @"\");
        return currentDataPath;
    }

    private void LOAD_checkCustomFolder()
    {
        string dataPath = Application.persistentDataPath;
        dataPath += "/Custom";
        if (Directory.Exists(dataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, dataPath);
            Directory.CreateDirectory(dataPath);
        }
    }

    private void LOAD_checkDebugFolder()
    {
        string dataPath = Application.persistentDataPath;
        dataPath += "/Debug";
        if (Directory.Exists(dataPath) == false)
        {
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isLOAD, DEBUG_mode.FILE_PATH, dataPath);
            Directory.CreateDirectory(dataPath);
        }
    }

}
