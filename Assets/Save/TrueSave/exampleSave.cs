using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class exampleSave : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameOfGalaxy;
    public TextMeshProUGUI numOfStars;
    public TextMeshProUGUI lastEdited;

    public string stellarisMapName;
    public string version;
    public string projectName;

    public string pathToLoad;

    public void setNameOfGalaxy(string nameOfGalaxy)
    {
        this.nameOfGalaxy.text = nameOfGalaxy;
    }
    public string getNameOfGalaxy()
    {
        return nameOfGalaxy.text;
    }
    public void setNumOfStarts(string numOfStars)
    {
        this.numOfStars.text = numOfStars;

    }
    public string getNumOfStars()
    {
        return numOfStars.text;
    }
    public void setLastEdited(string lastEdited)
    {
        this.lastEdited.text = lastEdited;

    }
    public string getLastEdited()
    {
        return lastEdited.text;
    }

    public void loadGalaxy()
    {
        cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text = projectName;
        cleanRef._cleanRef._UI_galaxy_info.galaxyName.text = nameOfGalaxy.text;
        cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text = stellarisMapName;
        cleanRef._cleanRef._UI_galaxy_info.version.text = version;
        cleanRef._cleanRef._UI_galaxy_info.title_galaxyName.text = nameOfGalaxy.text;


        saveEncapsulator._saveEncapsulator.saveSection.gameObject.SetActive(false);
        SaveManager._SaveManager.load(pathToLoad);
    }
}
