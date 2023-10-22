using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place scipt on objects that have ISaveable to save them
/// </summary>
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private System.Guid id;
    public System.Guid Id => id;

    public int priority;
    public bool ignore;
    public spawnTypes spawnType;

    public void setID(System.Guid id)
    {
        this.id = id;
    }

    [Serializable]
    public struct SaveableEntityData
    {
        public System.Guid id;
        public int priority;
        public bool ignore;
        public spawnTypes spawnType;
        public Dictionary<string, object> state;
    }

    /// <summary>
    /// Gets all ISaveable scipts in GameObject, puts it in a Dictionary, and returns it
    /// </summary>
    /// <returns></returns>
    public object saveState()
    {
        //Make new Dictionary
        SaveableEntityData newEntityData = new SaveableEntityData();

        //Setting the values
        newEntityData.state = new Dictionary<string, object>();
        newEntityData.id = this.id;
        newEntityData.priority = this.priority;
        newEntityData.ignore = this.ignore;
        newEntityData.spawnType = this.spawnType;

        //Gets all ISaveables in gameObject;
        ISaveable[] temp = GetComponents<ISaveable>();

        for(int i = 0;i<temp.Length;i++)
        {
            //Gets the location in the Dictionary of that scriptType and stores the save data inside of it 
            newEntityData.state[temp[i].GetType().ToString()] = temp[i].saveState();
        }
        //Returns the compiled save data
        return newEntityData;
    }

    /// <summary>
    /// Sets all ISaveable scipts in a GameObject to its corresponding data in a file
    /// </summary>
    /// <param name="state"></param>
    public void loadState(object state)
    {
        //Make new Dictionary from state
        SaveableEntityData newEntityData = (SaveableEntityData)state;

        //Setting the values;
        this.id = newEntityData.id;
        this.priority = newEntityData.priority;
        this.ignore = newEntityData.ignore;
        this.spawnType = newEntityData.spawnType;




        //Gets all ISaveables in gameObject;
        ISaveable[] temp = GetComponents<ISaveable>();
        for (int i = 0; i < temp.Length; i++)
        {
            //Gets the Dictionary key and stores it inside of typeName
            string typeName = temp[i].GetType().ToString();
            //If it is there then store savedState (savedData) in the scirpt with ISaveable
            if (newEntityData.state.TryGetValue(typeName,out object savedState))
            {
                temp[i].loadState(savedState);
            }
        }
    }


}
