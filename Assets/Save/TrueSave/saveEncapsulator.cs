using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using AnotherFileBrowser.Windows;
using System.Runtime.Serialization.Formatters.Binary;

public class saveEncapsulator : MonoBehaviour
{
    public string mainDataPath;
    public string string_fileOpened;
    public static saveEncapsulator _saveEncapsulator;

    [Header("UI Spawn")]
    public Transform childHolder;
    public GameObject example_Save;

    [Header("Unity Looks")]
    public GameObject loadSection;
    public GameObject newProjectSection;
    public GameObject newGalaxySextion;
    public GameObject loadButton;
    public GameObject newButton;
    public GameObject saveSection;
    public GameObject newGalaxyInfo;
    public GameObject newGalaxyButton;

    [Header("EditorTempVals")]
    public string temp_ProjectName;
    public string temp_EditorName;
    public string temp_StellarisName;
    public string temp_StellarisVersion;




    private void Awake()
    {
        _saveEncapsulator = this;
    }

    private void Start()
    {
        //Set default values
        temp_ProjectName = "Never Going";
        temp_EditorName = "To Give";
        temp_StellarisName = "You Up";
        temp_StellarisVersion = "3.6.0";
        mainDataPath = Application.persistentDataPath + "/Saves";
    }

    public void OpenProjectView(bool shouldNewGalaxy)
    {
        var bp = new BrowserProperties();
        bp.filter = "Lemon files (*.lemons) | *.lemons";
        bp.filterIndex = 0;


        new FileBrowser().OpenFileBrowser(bp, mainDataPath, path =>
         {
             StartCoroutine(fileOpened(shouldNewGalaxy, path));
         });
    }

    IEnumerator fileOpened(bool newGalaxy,string bob)
    {
        if(newGalaxy == false)
        {
            openProject(bob);
        }
        else
        {
            createNewGalaxy(bob);
            newButton.SetActive(false);
            loadButton.SetActive(false);
        }
        yield return null;
    }

    public void createNewGalaxy(string filePathFileName)
    {
        stellarisProject bob;

        using (FileStream stream = File.Open(filePathFileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            bob = (stellarisProject)formatter.Deserialize(stream);
        }

        if (bob.GalaxyData == null || bob.GalaxyData.Count == 0)
        {
            Debug.LogError("Project Is Empty");
            return;
        }
        else
        {
            newGalaxyInfo.SetActive(true);
            temp_ProjectName = bob.projectName;
        }
    }

    public void openProject(string filePathFileName)
    {
        stellarisProject bob;

        using (FileStream stream = File.Open(filePathFileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            bob = (stellarisProject)formatter.Deserialize(stream);
        }

        if(bob.GalaxyData == null || bob.GalaxyData.Count == 0)
        {
            //Open a default galaxy;
            return;
        }
        else
        {
            //Remove any past galaxy icons
            int limit = 100;
            for(int i = 0;i<childHolder.childCount;i++)
            {
                if(childHolder.GetChild(i).gameObject.activeInHierarchy == true)
                {
                    Destroy(childHolder.GetChild(i).gameObject);
                }
                if (i > limit)
                    return;
            }

            ////Check if there are any invalid files that the stellarisProject was not holding
            //string[] tempFilesInDirectory = Directory.GetFiles(filePathFileName.Substring(0, filePathFileName.LastIndexOf('\\')));
            //List<string> goodFiles = new List<string>();

            //for(int i = 0;i< tempFilesInDirectory.Length;i++)
            //{
            //    string outPutTemp = tempFilesInDirectory[i].Substring(tempFilesInDirectory[i].LastIndexOf('\\') + 1);
            //    if(outPutTemp.Contains(".lemons") == false)
            //    {
            //        outPutTemp = outPutTemp.Substring(0, outPutTemp.LastIndexOf('.'));
            //        goodFiles.Add(outPutTemp);
            //    }
            //}
            //Debug.Log("good");
            ////Checks to see if it mateches
            //for(int i = 0;i<goodFiles.Count;i++)
            //{
            //    bool isNotThere = true;
            //    for(int o = 0;o<bob.GalaxyData.Count;o++)
            //    {
            //        if(goodFiles[i] == bob.GalaxyData[o].nameOfGalaxy)
            //        {
            //            isNotThere = false;
            //        }
            //    }
            //    Debug.Log("good2");

            //    if (isNotThere == true)
            //    {
            //        Debug.Log("good3");

            //        for (int o = 0; o < tempFilesInDirectory.Length; o++)
            //        {
            //            Debug.Log("good4");

            //            if (tempFilesInDirectory[o].Contains(goodFiles[i] + ".lemon"))
            //            {
            //                Debug.Log("good4");

            //                using (FileStream stream = File.Open(tempFilesInDirectory[o], FileMode.Open))
            //                {
            //                    BinaryFormatter formatter = new BinaryFormatter();
            //                    Debug.Log(formatter.Deserialize(stream).GetType() + "\n filePAth-"+ tempFilesInDirectory[o]);
            //                    exampleSave temp_exampleSave = (exampleSave)formatter.Deserialize(stream);
            //                    //bob.addGalaxy(System.Convert.ToInt32(temp_exampleSave.getNumOfStars()),
            //                    //    temp_exampleSave.getLastEdited(),
            //                    //    temp_exampleSave.getNameOfGalaxy(),
            //                    //    temp_exampleSave.stellarisMapName,
            //                    //    temp_exampleSave.version);
            //                    temp_exampleSave.projectName = bob.projectName;
            //                    stream.Close();
            //                    using (FileStream stream2 = File.Open(tempFilesInDirectory[o], FileMode.Create))
            //                    {
            //                        formatter.Serialize(stream, temp_exampleSave);
            //                        stream2.Close();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    //If it does not then add a newButton with default values
            //}


            //Show all Galaxys in the Stellaris Project
            GameObject[] tempHolder = new GameObject[bob.GalaxyData.Count];
            for (int i = 0; i < bob.GalaxyData.Count;i++)
            {
                tempHolder[i] = Instantiate(example_Save);
                tempHolder[i].SetActive(true);
                tempHolder[i].transform.SetParent(childHolder,false);
                exampleSave tempSaveData = tempHolder[i].GetComponent<exampleSave>();

                tempSaveData.setLastEdited(bob.GalaxyData[i].lastEdited);
                tempSaveData.setNumOfStarts(bob.GalaxyData[i].starCount + "");
                tempSaveData.setNameOfGalaxy(bob.GalaxyData[i].nameOfGalaxy);
                tempSaveData.stellarisMapName = bob.GalaxyData[i].stellarisMapTitle;
                tempSaveData.version = bob.GalaxyData[i].version;
                tempSaveData.projectName = bob.projectName;

                int indexOfPeriod = filePathFileName.LastIndexOf('\\');
                string subed = filePathFileName.Substring(0, indexOfPeriod) + "\\" + tempSaveData.nameOfGalaxy.text + ".lemon";
                Debug.Log(subed);
                tempSaveData.pathToLoad = subed;
            }
        }
    }


    public void loadGalaxy(exampleSave galaxyToLoad)
    {
        int lastIndex = string_fileOpened.LastIndexOf(".");
        string temp = string_fileOpened.Substring(0, lastIndex-1);

        //Now it should like C:"FilePath"/SaveFolder
        string fileToLoad = temp + "/" + galaxyToLoad.name;

        //Call method that takes it and loads it
        gameObject.SetActive(false);
    }


    #region Unity UI connectors

    public void next()
    {
        newGalaxySextion.SetActive(true);
        newProjectSection.SetActive(false);
    }
    public void back()
    {
        newGalaxySextion.gameObject.SetActive(false);
        newProjectSection.SetActive(true);
        loadButton.SetActive(false);
        newGalaxyButton.SetActive(false);
    }

    public void quitNewProject()
    {
        newProjectSection.SetActive(false);
        newGalaxyButton.SetActive(false);
        loadButton.SetActive(true);
        newGalaxyButton.SetActive(true);
        newButton.SetActive(true);
        newGalaxyInfo.SetActive(false);

    }
    public void done()
    {
        //Set four values to ones un clean ref
       // UI_menus._UI_menus.galaxy_info.gameObject.SetActive(true);

        //Project Name
        cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text = temp_ProjectName;
        //Editor Name For The Galaxy
        cleanRef._cleanRef._UI_galaxy_info.galaxyName.text = temp_EditorName;
        //Stellaris Name For The Galaxy
        cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text = temp_StellarisName;
        //Steam Version
        cleanRef._cleanRef._UI_galaxy_info.version.text = temp_StellarisVersion;
        


        //Just for player conveince is the Editor name of the galaxy again
        cleanRef._cleanRef._UI_galaxy_info.title_galaxyName.text = temp_EditorName;
       // UI_menus._UI_menus.galaxy_info.gameObject.SetActive(false);
        loadSection.transform.parent.gameObject.SetActive(false);
    }

    public void updateProjectName(string updatedInfo)
    {
        temp_ProjectName = updatedInfo;
    }
    public void updateEditorName(string updatedInfo)
    {
        temp_EditorName = updatedInfo;
    }
    public void updateStellarisName(string updatedInfo)
    {
        temp_StellarisName = updatedInfo;
    }
    public void updateStellarisVersion(string updatedInfo)
    {
        temp_StellarisVersion = updatedInfo;
    }

    #endregion
    //List that holds all projects -
    //In projects it has a list of galaxySaves
    //once we find the correct file send it to SaveManager

    //Transformed Idea
    //Have a filePath that holds all "projects"
    //"projects" are files that hold information such as name, last edited, picture of galaxy
    //In the folder that the projects are in there will be a folder with the same name of the project that will hold the galaxy data

}
