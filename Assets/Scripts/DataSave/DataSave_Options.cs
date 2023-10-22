using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DataSave_Options", menuName = "GalaxyBlacksmith/DataSave_Options", order = 1)]

public class DataSave_Options : ScriptableObject
{
    //This is used in the MainMenu to know if the user should go through the tutorial of adding in the two needed filepaths
    public bool isFirstTimePlaying = true;
    //These two are used to save the filePath the user enters in
    public string stellarisFilePath_steam = null;
    public string stellarisFilePath_documents = null;


    //When get back work on the three unqiue screens for all of em pictures to get file paths to work well
}
