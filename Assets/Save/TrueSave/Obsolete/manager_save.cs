using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class manager_save : MonoBehaviour
{
    public dataSave_Galaxy saveFileOne;
    public saveGridNode temper;
    public GameObject nodeToSave;
    public string pathToSave;

    public static manager_save _manager_save;

    public saveFile One_saveFile;

    private void Awake()
    {
        _manager_save = this;
        pathToSave = Application.persistentDataPath;
    }


    //public void save()
    //{

    //    saveFileOne.example = Instantiate(place_manager._place_manager.parentHolder.gameObject);
    //    saveFileOne.nodePos = new List<Vector3>(place_manager._place_manager.posOfCubes);
    //    saveFileOne.example.SetActive(false);

    //}


    public void save()
    {
        One_saveFile.saveProject("exampleTest1.lemon");
    }
    public void load()
    {

    }




    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            save();
        }
    }

    //public void load()
    //{
    //    if(place_manager._place_manager.parentHolder.gameObject != null)
    //    {
    //        Destroy(place_manager._place_manager.parentHolder.gameObject);
    //        GameObject temp = Instantiate(saveFileOne.example);
    //        Destroy(saveFileOne.example);

    //        temp.transform.SetParent(place_manager._place_manager.transform);
    //        place_manager._place_manager.parentHolder = temp.transform;

    //        //Copy of pos of vectors
    //        place_manager._place_manager.posOfCubes = new List<Vector3>(saveFileOne.nodePos);
    //        //Copy gameObjects



    //        place_manager._place_manager.grid_placedThings = new List<GameObject>();
    //        for (int i = 0;i<temp.transform.childCount;i++)
    //        {
    //            place_manager._place_manager.grid_placedThings.Add(temp.transform.GetChild(i).gameObject);
    //        }



    //        temp.SetActive(true);
    //    }
    //}

    //private void Update()
    //{
    //    //C:\Users\Potato2\Desktop\UnityGames\Stellaris\StellarisFinalGen\Assets\Resources
    //    pathToSave = pathToSave + "test2.lemon";
    //    //Save
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(pathToSave, FileMode.Create);

    //        saveGridNode[] bob = new saveGridNode[100];
    //        for (int i = 0; i < 100; i++)
    //        {
    //            bob[i] = new saveGridNode();
    //            bob[i].saveNewNode(nodeToSave);
    //        }


    //        // temper = new saveGridNode();
    //        // temper.saveNewNode(nodeToSave);

    //        Debug.Log("Before node file is ");
    //        Debug.Log(bob[50].save_systemData.printOutPlanet(bob[50].save_systemData.planets));


    //        formatter.Serialize(stream, bob);
    //        stream.Close();
    //    }
    //    //Load
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        if (File.Exists(pathToSave))
    //        {
    //            BinaryFormatter formatter = new BinaryFormatter();
    //            FileStream stream = new FileStream(pathToSave, FileMode.Open);


    //            saveGridNode[] temp = formatter.Deserialize(stream) as saveGridNode[];
    //            Debug.Log("After node file is ");
    //            Debug.Log(temp[50].save_systemData.printOutPlanet(temp[50].save_systemData.planets));


    //            stream.Close();
    //        }
    //        else
    //        {
    //            Debug.LogError("file path does not exisit");
    //        }
    //    }
    //}
}
