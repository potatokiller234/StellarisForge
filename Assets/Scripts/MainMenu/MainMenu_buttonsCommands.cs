using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu_buttonsCommands : MonoBehaviour
{
   // public DataSave_Options playerData;
    public string tempSteamPath;
    public string tempDocumentsPath;

    public TMP_InputField steamInput;
    public TMP_InputField documentsInput;

    public GameObject steamFail;
    public GameObject documentsFail;

    public GameObject steamCanvas;
    public GameObject documentsCanvas;
    public GameObject mainMenuCanvas;
    public GameObject f1_Canvas;

    private void Update()
    {
        //Tips
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (f1_Canvas.activeSelf == true)
                f1_Canvas.SetActive(false);
            else
                f1_Canvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (f1_Canvas.activeSelf == true)
                f1_Canvas.SetActive(false);
            else
            {
                mainMenuCanvas.SetActive(true);
                steamCanvas.SetActive(false);
                documentsCanvas.SetActive(false);
            }
        }
    }


    public void MainMenu_start()
    {

        int outputOfCheck = (int)checkIfFilePathsAreGood();
        Debug.Log(outputOfCheck);
        switch (outputOfCheck)
        {
            case 0:
                Debug.Log("Swich To Smithing Area");
                idGenerator._idGenerator.save();
                SceneManager.LoadScene(1);
                break;
            case 1:
                //Swich to screen where you need to re-input the steam filePath
                mainMenuCanvas.SetActive(false);
                steamCanvas.SetActive(true);
                break;
            case 2:
                //Swich to screen where you need to re-input the documents filePath
                mainMenuCanvas.SetActive(false);
                steamCanvas.SetActive(true);
                break;
            case 3:
                //Swich to screen where you need to input both filePaths
                mainMenuCanvas.SetActive(false);
                steamCanvas.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid Number from Range 0-3 (Inclusive) " + outputOfCheck);
                break;
        }


    }
    public void MainMenu_quit()
    {
        Application.Quit();
    }
    public void MainMenu_options()
    {
        //Does nothing at the moment        
    }
    public void MainMenu_filePath_steamDone()
    {
        Debug.Log(tempSteamPath + @"\common");
        if(Directory.Exists(tempSteamPath + @"\common"))
        {
            idGenerator._idGenerator.setSteamPath(tempSteamPath);
            steamInput.text = "Works Great Boss";
            steamFail.SetActive(false);
            steamCanvas.SetActive(false);
            documentsCanvas.SetActive(true);
        }
        else
        {
            steamInput.text = "";
            StartCoroutine(disableEnable(steamFail.gameObject, 2f));
        }
    }

    public void setTemps(GameObject thingToG)
    {
        tempDocumentsPath = thingToG.GetComponent<TMP_InputField>().text;
        tempSteamPath = tempDocumentsPath;
    }




    IEnumerator disableEnable(GameObject thingToEnable, float time)
    {
        thingToEnable.SetActive(true);
        yield return new WaitForSeconds(time);
        thingToEnable.SetActive(false);
    }
    public void MainMenu_filePath_documentsDone()
    {
        //Check if file path for steam is done, if done go to next screen (editor or steam filePath)
        if (Directory.Exists(tempDocumentsPath + @"\crashes"))
        {
            idGenerator._idGenerator.setDocumentsPath(tempDocumentsPath);
            documentsInput.text = "Works Great Boss";
            documentsFail.SetActive(false);
            documentsCanvas.SetActive(false);
            Debug.Log("Swiching to Smithing Area");
            idGenerator._idGenerator.save();
            SceneManager.LoadScene(1);
        }
        else
        {
            documentsInput.text = "";
            StartCoroutine(disableEnable(documentsFail.gameObject, 2f));
        }
    }


    public bool MainMenu_doesExist_steamPath()
    {
        if (Directory.Exists(idGenerator._idGenerator.getSteamPath() + @"\common"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool MainMenu_doesExist_documentPath()
    {
        if (Directory.Exists(idGenerator._idGenerator.getDocumentsPath() + @"\crashes"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Used to check if the the two files paths are correct
    //0 means that 
    public filePathGood checkIfFilePathsAreGood()
    {
        idGenerator._idGenerator.load();
        //Checks to see if saved data is the correct path
        bool IsStellarisFilePath_documents = MainMenu_doesExist_documentPath();
        bool IsstellarisFilePath_steam = MainMenu_doesExist_steamPath();


        //If not returns what should be done
        if(IsStellarisFilePath_documents == true && IsstellarisFilePath_steam == true)
        {
            return filePathGood.bothPass;
        }
        else if(IsstellarisFilePath_steam == false)
        {
            return filePathGood.steamFail;
        }
        else if(IsStellarisFilePath_documents == false)
        {
            return filePathGood.documentsFail;
        }
        else
        {
            Debug.LogError("Warning You done messed up some how, BONER man");
            return filePathGood.boned;
        }
    }
}


public enum filePathGood
{
    bothPass = 0,
    steamFail = 1,
    documentsFail = 2,
    boned = 3
}
