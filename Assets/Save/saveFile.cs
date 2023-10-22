using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName = "new saveFile", menuName = "GalaxyBlacksmith/saveFile", order = 1)]
public class saveFile : ScriptableObject
{
    //Holds Every Save Possible
  //  List<saveProject> saved_project;
   // public int lastOpenProject = -1;
    public saveProject currentOpenedProject;




    /// <summary>
    /// Called when user did not load a past map file
    /// </summary>
    public void openDefaultProject()
    {
        //Set default values for 
        //Realy has no use because it is just like opening a new scene
    }
    //Loads new projectFile when selected in the editor, uses the fileName to read the .LEMON file
    public void openProjectFile(string fileName)
    {
        //#region Get Path, error checks, reads file and stores it inside currentOpenedProject
        //string truePath = manager_save._manager_save.pathToSave + fileName;

        ////If file path does not exist give error
        //if (File.Exists(truePath) == false)
        //{
        //    Debug.LogError("Invalid file name-" + fileName + " in path " + truePath);
        //    return;
        //}
        ////Now we know file exisits so we can do cool things
        //BinaryFormatter formatter = new BinaryFormatter();
        //FileStream stream = new FileStream(truePath, FileMode.Open);
        //currentOpenedProject = formatter.Deserialize(stream) as saveProject;
        //stream.Close();

        ////Spawn in all the nodes
        ////Spawn in all hyperlanes
        ////Fix connections with UI_buttons (or not)
        //int lastOpened = currentOpenedProject.loadGalaxy();

        ////Spawned in default node
        ////Loads data from save into Node
        ////Loads node into gridNodes cool dude with its correct ID
        //for(int i = 0;i<currentOpenedProject.saved_galaxy[lastOpened].nodes.Count;i++)
        //{
        //    //Ref currentOpenedProject.saved_galaxy[i]
        //    GameObject newNode = Instantiate(place_manager._place_manager.defaultSphere);
        //    currentOpenedProject.saved_galaxy[lastOpened].nodes[i].loadNewNode(newNode);
        //    place_manager._place_manager.gridNode.addOldFella(newNode, newNode.GetComponent<gridData>().ID);
        //    //currentOpenedProject.saved_galaxy[i]
        //}
        //for (int i = 0; i < currentOpenedProject.saved_galaxy[lastOpened].hyperlanes.Count; i++)
        //{
        //    //Ref currentOpenedProject.saved_galaxy[i]
        //    GameObject newHyperlane = Instantiate(place_manager._place_manager.defaultHyperlane);
        //    currentOpenedProject.saved_galaxy[lastOpened].hyperlanes[i].loadNewHyperlane(newHyperlane);
        //    place_manager._place_manager.hyperlane.addOldFella(newHyperlane, newHyperlane.GetComponent<gridHyperlane>().ID);
        //    //currentOpenedProject.saved_galaxy[i]
        //}
        //for (int i = 0; i < currentOpenedProject.saved_galaxy[lastOpened].UI_nodes.Count; i++)
        //{
        //    //Ref currentOpenedProject.saved_galaxy[i]

        //    //Call buttons to be spawned in using typical way
        //    //Search through all those buttons to find if they match
        //    //IF match copy data
        //    //if not then ad it to fella
        //    //currentOpenedProject.saved_galaxy[lastOpened].hyperlanes[i].loadNewHyperlane(newHyperlane);
        //   // place_manager._place_manager.hyperlane.addOldFella(newHyperlane, newHyperlane.GetComponent<gridHyperlane>().ID);
        //    //currentOpenedProject.saved_galaxy[i]
        //}

        //#endregion
    }
    public void saveProject(string fileName)
    {
        string truePath = manager_save._manager_save.pathToSave + fileName;

        //Need a check to see if this is new or not project
        //If new then saveNewProject
        //Now we know we got the correct one

        //First make vars
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        //If exist make new file if not update file 
        if (File.Exists(truePath) == false)
        {
             Debug.LogWarning("File does not exist, so we shall make a new " + fileName + " in " + truePath);
             formatter = new BinaryFormatter();
             stream = new FileStream(truePath, FileMode.CreateNew);
        }
        else
        {
             formatter = new BinaryFormatter();
             stream = new FileStream(truePath, FileMode.Create);
        }

        //Check if this data is in a currentOpenedProject if null then make new currentOpened
        if (currentOpenedProject == null)
        {
            currentOpenedProject = new saveProject();
            currentOpenedProject.saveOpenGalaxy();
            //saved_project.Add(currentOpenedProject);
        }
        //If not then update Save
        else
        {
            currentOpenedProject.saveOpenGalaxy();
        }
        formatter.Serialize(stream, currentOpenedProject);
        stream.Close();
    }



    //Projects
        //Many Galaxy Examples
        //Many Custom Sol

    //Using an arbitrary id will get the correct project
    //Once it gets the project it will call its Load or Save Command
    //Saving Done
}
