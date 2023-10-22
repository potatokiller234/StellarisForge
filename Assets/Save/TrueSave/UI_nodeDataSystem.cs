using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_nodeDataSystem : MonoBehaviour, ISaveable
{
    public data_system Data_System;
    TextMeshProUGUI UI_text;
    public string initName;
    public UI_nodeDataFile cool_parent;
    public Color color = new Color(.4f,.4f,.4f);
    //List<gridData> spawnedKids;
    public List<System.Guid> spawnedKids;

    //ID
    public System.Guid ID;

    private void Start()
    {
        if(spawnedKids == null)
            spawnedKids = new List<System.Guid>();
      //  spawnedKids = new List<int>();
    }

    #region Cool Beans
    public void UI_setNewNodeDataSystem(data_system Data_System, TextMeshProUGUI UI_text)
    {
        this.Data_System = Data_System;
        this.UI_text = UI_text;
        initName = Data_System.initializerName;
        UI_text.text = Data_System.initializerName;
    }

    public data_system getSystemData()
    {
        return Data_System;
    }

    public void removeKid(System.Guid oneToRemove)
    {
        spawnedKids.Remove(oneToRemove);
    }
    public void addKid(System.Guid kitToAdd)
    {
        spawnedKids.Add(kitToAdd);
    }

    public void updateKidColors(Color newColor)
    {
        GameObject tempChecker;
        for(int i = 0;i<spawnedKids.Count;i++)
        {
            tempChecker = place_manager._place_manager.supreamDude.getID(spawnedKids[i],false);
            if(tempChecker == null)
            {
                spawnedKids.RemoveAt(i);
                i--;
            }
            else
            {
                tempChecker.GetComponent<gridData>().setUpColor(newColor);
            }
        }

    }
    #endregion
    #region Save/Load
    [System.Serializable]
    private struct SaveData
    {
        //ID 
        public System.Guid save_ID;
        //Stellaris Information
        public data_system save_Data_System;
        public string save_initName;
        //Unity Features
        public saveColor save_Color;
        public List<System.Guid> save_spawnedKids;
    }

    public object saveState()
    {
        Debug.Log("Saved-" + spawnedKids.Count);

        return new SaveData()
        {
            save_Data_System = getSystemData(),
            save_initName = initName,
            save_Color = new saveColor(color),
            save_spawnedKids = spawnedKids,
            save_ID = ID
        };

    }

    public void loadState(object state)
    {

        SaveData saveData = (SaveData)state;

        Data_System = saveData.save_Data_System;
        initName = saveData.save_initName;
        color = saveData.save_Color.getColor();
        spawnedKids = new List<System.Guid>(saveData.save_spawnedKids);
        ID = saveData.save_ID;

       // Debug.Log("Loaded Kids-" + spawnedKids.Count + " on Object-" + initName);

    }
    #endregion


}
