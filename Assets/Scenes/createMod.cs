using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class createMod : MonoBehaviour
{
    public string[] version;
    public int versionNum;
    public int supportedVersion;
    public List<string> tags;
    public string nameOfMod = "NoName";
    public string userGavePath;
    public bool removeAllMods = false;

    // Start is called before the first frame update
    void Start()
    {
        if(removeAllMods == true)
        {
            removeAllFiles();
        }

        //Making the time stamp of currentTime acceptable for file explorer
        string currentTime = System.DateTime.Now.ToString();
        currentTime = currentTime.Replace(' ', '_');
        currentTime = currentTime.Replace('/', '-');
        currentTime = currentTime.Replace(':', '.');


        //Makes a new folder with the time stamp and mod name for the user
        string folderPath = Application.dataPath + @"\Resources\" + nameOfMod +"_" + currentTime;
        Directory.CreateDirectory(folderPath);


        //Creates the folder path for the mod
        string path = folderPath + @"\" + nameOfMod + ".mod";


        //Opens a new stream writer and sets up the .mod file that stellaris would auto generate
        StreamWriter modCreator = new StreamWriter(path);
        modCreator.WriteLine("version=\"" + version[versionNum] + "\"");
        modCreator.WriteLine("tags={");

        for(int i = 0;i<tags.Count;i++)
        {
            modCreator.WriteLine("	\"" + tags[i] + "\"");
        }
        modCreator.WriteLine("}");

        modCreator.WriteLine("name=\"" + nameOfMod + "\"");
        modCreator.WriteLine("supported_version=\"" + version[supportedVersion] + "\"");
        //path="C:/Users/Potato2/Documents/Paradox Interactive/Stellaris/mod/peep"

        userGavePath = userGavePath.Replace(@"\",@"/");
        modCreator.WriteLine("path=\"" + userGavePath + @"/" +nameOfMod + "\"");


        //Closes the StreamWriter
        modCreator.Close();

        //Creates a new folder with the same descipter like how stellaris does
        string folderPathMod = folderPath + @"\" + nameOfMod;
        Directory.CreateDirectory(folderPathMod);
        File.Copy(path, folderPathMod + @"\descriptor.mod");

    }

    //Removes all files
    public void removeAllFiles()
    {
        //Removes all files in the Resources file path
        string filePath = Application.dataPath + @"\Resources";
        System.IO.DirectoryInfo di = new DirectoryInfo(filePath);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }

 
}
