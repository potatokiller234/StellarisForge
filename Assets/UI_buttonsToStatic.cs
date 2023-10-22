using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_buttonsToStatic : MonoBehaviour
{
    public List<string> modsToMove;
    public List<string> filesToMove;
    public List<string> solarSystemInitsToMove;
    List<UI_nodeDataSystem> papasToRemove;


    public UI_parsedToButtons Instance_UI_parsedToButtons;

    public GameObject staticContainer;


    public List<GameObject> dumbMoveFirst;

    public void moveToStatic()
    {
        Debug.Log(this.name);
        modsToMove = new List<string>();
        filesToMove = new List<string>();
        solarSystemInitsToMove = new List<string>();

        papasToRemove = new List<UI_nodeDataSystem>();




        //This "mod" holds the random init
        modsToMove.Add("Default Initializers");
        //This "file" to move holds the normal empire inits
        //filesToMove.Add("custom_starting_initializers");
        //This "system" to move to

        solarSystemInitsToMove.Add("random_empire_init_06");
        solarSystemInitsToMove.Add("random_empire_init_05");
        solarSystemInitsToMove.Add("random_empire_init_04");
        solarSystemInitsToMove.Add("random_empire_init_03");
        solarSystemInitsToMove.Add("random_empire_init_02");
        solarSystemInitsToMove.Add("random_empire_init_01");

        solarSystemInitsToMove.Add("custom_starting_init_01");
        solarSystemInitsToMove.Add("custom_starting_init_02");
        solarSystemInitsToMove.Add("custom_starting_init_03");
        solarSystemInitsToMove.Add("custom_starting_init_04");
        solarSystemInitsToMove.Add("custom_starting_init_05");
        solarSystemInitsToMove.Add("custom_starting_init_06");



        for (int i = 0; i < solarSystemInitsToMove.Count; i++)
        {
            moveSystemInit(solarSystemInitsToMove[i]);
        }
        createNewFileHeader("Empire Initializers");
        for (int i = 0; i < filesToMove.Count; i++)
        {
            moveFile(filesToMove[i]);
        }
        for (int i = 0; i < modsToMove.Count; i++)
        {
            moveMod(modsToMove[i]);
        }
        removeAllSystemFromParent();
      //  removeSystemInit("example_initializer");
        removeFile("example");
        removeFile("empire_initializers");
        removeFile("custom_starting_initializers");



        for (int i = dumbMoveFirst.Count - 1; i >= 0; i--)
        {
            dumbMoveFirst[i].transform.SetAsFirstSibling();
        }
    }

    private void createNewFileHeader(string fileHeaderName)
    {
        for(int i = 0;i<staticContainer.transform.childCount;i++)
        {
            if (staticContainer.transform.GetChild(i).name == fileHeaderName)
                return;
        }
        float ySize = cleanRef._cleanRef._UI_parsedToButtons.yFileSize;
        Color colorForFile = cleanRef._cleanRef._UI_parsedToButtons.colorFile;
        GameObject exampleButton = cleanRef._cleanRef._UI_parsedToButtons.example_button;

        GameObject newFileThing = Instantiate(exampleButton);
        newFileThing.name = fileHeaderName;
        cleanRef._cleanRef._UI_parsedToButtons.updateLooks(newFileThing, ySize, colorForFile);
        newFileThing.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = fileHeaderName;
        newFileThing.gameObject.transform.SetParent(staticContainer.transform,false);
        newFileThing.transform.SetAsFirstSibling();
        newFileThing.SetActive(true);
    }
    private void moveMod(string modToMove)
    {
        for (int i = 0; i < staticContainer.transform.childCount; i++)
        {
            if (staticContainer.transform.GetChild(i).name == modToMove)
            {
                return;
            }
        }

        foreach (UI_nodeDataMod mod in Instance_UI_parsedToButtons.UI_nodeDataModeHolder)
        {
            if (mod.ModName == modToMove)
            {
                if (mod.UI_nodeDataFileHolder.Count == 0)
                    continue;
                cleanRef._cleanRef._UI_parsedToButtons.UI_nodeDataModeHolder.Remove(mod);
                foreach (UI_nodeDataFile file in mod.UI_nodeDataFileHolder)
                {
                    moveFile(file);
                }
                mod.name = modToMove;
                mod.transform.SetParent(staticContainer.transform,false);
                mod.transform.SetAsFirstSibling();
                return;
            }
        }
    }

    private void moveFile(UI_nodeDataFile fileToMove)
    {
        string fileToFind = fileToMove.fileName;
        //Debug.Log(fileToMove.fileName);
        for (int i = 0;i<fileToMove.UI_nodeDatasystemHolder.Count;i++)
        {
            moveSystemInit(fileToMove.UI_nodeDatasystemHolder[i]);
        }
        fileToMove.gameObject.transform.SetParent(staticContainer.transform,false);
        fileToMove.transform.SetAsFirstSibling();
    }

    private void moveFile(string fileToMove)
    {
        UI_nodeDataFile temp = getFileInit(fileToMove);

        if (temp == null)
            return;
        foreach (UI_nodeDataSystem system in temp.UI_nodeDatasystemHolder)
        {
            moveSystemInit(system);
        }
        temp.gameObject.transform.SetParent(staticContainer.transform,false);
        temp.transform.SetAsFirstSibling();
        return;
    }

    private UI_nodeDataFile getFileInit(string fileToMove)
    {
        //Is looking though the list of mods
        if (Instance_UI_parsedToButtons.UI_nodeDataModeHolder.Count == 0)
            return null;
        foreach (UI_nodeDataMod mod in Instance_UI_parsedToButtons.UI_nodeDataModeHolder)
        {
            if (mod.UI_nodeDataFileHolder.Count == 0)
                continue;
            foreach (UI_nodeDataFile file in mod.UI_nodeDataFileHolder)
            {
                //if (file.UI_nodeDatasystemHolder.Count == 0)
                //    continue;

                if (file.fileName == fileToMove)
                {
                    return file;
                }
            }
        }
        return null;
    }

    private void removeFile(string fileToRemove)
    {
        UI_nodeDataFile temp = getFileInit(fileToRemove);

        if (temp == null)
        {
            Debug.LogWarning("it was null");
            return;
        }
        while (temp.UI_nodeDatasystemHolder.Count != 0)
        {
            UI_nodeDataSystem bob = temp.UI_nodeDatasystemHolder[0];
            temp.UI_nodeDatasystemHolder.RemoveAt(0);
            Destroy(bob.gameObject);
        }
        temp.cool_parent.UI_nodeDataFileHolder.Remove(temp);
        Destroy(temp.gameObject);
    }


    private void moveSystemInit(string systemToMove)
    {
        for (int i = 0; i < staticContainer.transform.childCount; i++)
        {
            if (staticContainer.transform.GetChild(i).name == systemToMove)
                return;
        }
        UI_nodeDataSystem temp = getSystemInit(systemToMove);
        if (temp == null)
            return;
        temp.gameObject.name = systemToMove;
        moveSystemInit(temp);
    }

    private void removeSystemInit(string systemToRemove)
    {
        UI_nodeDataSystem temp = getSystemInit(systemToRemove);
        if (temp == null)
            return;
        temp.cool_parent.UI_nodeDatasystemHolder.Remove(temp);
        Destroy(temp.gameObject);
    }

    private UI_nodeDataSystem getSystemInit(string systemToGet)
    {
        //Is looking though the list of mods
        if (Instance_UI_parsedToButtons.UI_nodeDataModeHolder.Count == 0)
            return null;
        foreach (UI_nodeDataMod mod in Instance_UI_parsedToButtons.UI_nodeDataModeHolder)
        {
            if (mod.UI_nodeDataFileHolder.Count == 0)
                continue;
            foreach (UI_nodeDataFile file in mod.UI_nodeDataFileHolder)
            {
                if (file.UI_nodeDatasystemHolder.Count == 0)
                    continue;
                foreach (UI_nodeDataSystem system in file.UI_nodeDatasystemHolder)
                {
                    if (system.initName == systemToGet)
                    {
                        return system;
                    }
                }
            }
        }

        return null;
    }

    private void moveSystemInit(UI_nodeDataSystem systemToMove)
    {
        papasToRemove.Add(systemToMove);
        systemToMove.gameObject.transform.SetParent(staticContainer.transform,false);
        systemToMove.transform.SetAsFirstSibling();

    }
    private void removeAllSystemFromParent()
    {
        for(int i = 0;i<papasToRemove.Count;i++)
        {
            papasToRemove[i].cool_parent.UI_nodeDatasystemHolder.Remove(papasToRemove[i]);
        }
    }
}
