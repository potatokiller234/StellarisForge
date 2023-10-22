using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherFileBrowser.Windows;
using System.IO;
using System.Threading.Tasks;

public class Parse_staticToUnity : MonoBehaviour
{
    public string persistentDataPath;
    public bool isGoingToDebug = false;
    public stellarisDataHolder staticGalaxyAST;


    public void openNewStatic()
    {
        var bp = new BrowserProperties();
        bp.filter = "Text files (*.txt) | *.txt";
        bp.filterIndex = 0;
        persistentDataPath = Application.persistentDataPath + "/Saves";
        new FileBrowser().OpenFileBrowser(bp, persistentDataPath, path =>
        {
            StartCoroutine(fileOpened(path));
        });
    }

    IEnumerator fileOpened(string path)
    {
        Task.Run(() =>
        {
            try
            {
                Debug.Log(path);
                FileInfo file = new FileInfo(path);

                if (file.Exists == false)
                {
                    Debug.LogError("A file does not exist at path " + path);
                }
                else
                {
                    staticGalaxyAST = parseCommands.parse_StandardRaw_to_ABT(file, persistentDataPath, false);
                }
                Debug.Log(staticGalaxyAST.printOutTree(staticGalaxyAST.infoHolder));
                //string r_output = "";
                //for(int i = 0;i<output.Count;i++)
                //{
                //    r_output += " |" + output[i] + "|";
                //}
                //Debug.Log(r_output);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        });
        yield return null;
    }
}
