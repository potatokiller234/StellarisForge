using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gridData : MonoBehaviour, ISaveable
{
    [Header("Stellaris Info")]
    public gridPlaceType placeType = gridPlaceType.nullPlace;
    public data_system systemData = null;
    [Header("Unity Info")]
    public Material currentMaterial;
    public Color color;
    public Color emissiveColor;
    public int emissiveNumber = 2;
    public System.Guid UI_father = System.Guid.Empty;
    public TextMeshPro text_nameOfSystem;


    public List<System.Guid> connectedHyperlanes;

    private GameObject nebulaRender;
    private GameObject wormHole;

    public List<System.Guid> connectedNodes;

    //Used for ID
    public System.Guid ID;

    private void Start()
    {
        if(connectedNodes == null)
        {
            connectedNodes = new List<System.Guid>();
        }
        if (connectedHyperlanes == null)
        {
            connectedHyperlanes = new List<System.Guid>();
        }
    }
    #region Married
    //Add new ref to current Node being connected node and new hyperlane
    public void addNode(System.Guid marriedNode, System.Guid marriedHyperlane)
    {
        connectedNodes.Add(marriedNode);
        //Used bc first node already has connections
        if(isAlreadyConnectedHyperlane(marriedHyperlane) == false)
            connectedHyperlanes.Add(marriedHyperlane);
    }
    public void addHyperlane(System.Guid marriedHyperlane)
    {
        connectedHyperlanes.Add(marriedHyperlane);
    }
    #endregion
    #region Divorce
    public void removeNode(System.Guid divorcedNode)
    {
        connectedNodes.Remove(divorcedNode);
    }
    public void removeHyperlane(System.Guid divorcedHyperlane)
    {
        for(int i = 0;i< connectedHyperlanes.Count;i++)
        {
            if (connectedHyperlanes[i] == divorcedHyperlane)
            {
                connectedHyperlanes.Remove(divorcedHyperlane);
                return;
            }
        }
        Debug.LogError("Could not find Hyperlane");
    }
    public void removeGridData()
    {
        for (int i = 0; i < connectedHyperlanes.Count; i++)
        {
            //Using the ids gets GameObject from placeManager to call the removeHyperlane command
            //Debug.Log(place_manager._place_manager.hyperlane.getCount());
            place_manager._place_manager.supreamDude.getID(connectedHyperlanes[i], true).GetComponent<gridHyperlane>().removeHyperlane();
            i--;
            Debug.Log(connectedHyperlanes.Count + " countOfConnect");
        }

        place_manager._place_manager.removeThingFromPlaceManager(this.gameObject, gridPlaceType.solarSystemInitializer);
        Destroy(this.gameObject);
    }
    #endregion
    #region Tools
    public bool isAlreadyConnectedNode(System.Guid areWeMarriedNode)
    {
        for(int i = 0;i<connectedNodes.Count;i++)
        {
            if (areWeMarriedNode == connectedNodes[i])
                return true;
        }
        return false;
    }

    public bool isAlreadyConnectedHyperlane(System.Guid areWeMarriedHyperlane)
    {
        for (int i = 0; i < connectedHyperlanes.Count; i++)
        {
            if (areWeMarriedHyperlane == connectedHyperlanes[i])
                return true;
        }
        return false;
    }
    private void setUpColor()
    {
        currentMaterial = this.gameObject.GetComponent<Renderer>().material;
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(.5f, .5f, .5f);
        currentMaterial.SetColor("_BaseColor", this.color);
        currentMaterial.SetColor("_EmissionColor",updateColor(emissiveColor,emissiveNumber));

    }
    public void setUpColor(Color color)
    {
        this.color = color;
        this.emissiveColor = color;

        currentMaterial = this.gameObject.GetComponent<Renderer>().material;
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(.5f, .5f, .5f);
        currentMaterial.SetColor("_BaseColor", this.color);
        currentMaterial.SetColor("_EmissionColor", updateColor(emissiveColor, emissiveNumber));
    }
    public Color updateColor(Color subjectColor, float intensityChange)
    {
        byte k_MaxByteForOverexposedColor = 191;
        // Get color intensity
        float maxColorComponent = subjectColor.maxColorComponent;
        float scaleFactorToGetIntensity = k_MaxByteForOverexposedColor / maxColorComponent;
        float currentIntensity = Mathf.Log(255f / scaleFactorToGetIntensity) / Mathf.Log(2f);

        // Get original color with ZERO intensity
        float currentScaleFactor = Mathf.Pow(2, currentIntensity);
        Color originalColorWithoutIntensity = subjectColor / currentScaleFactor;

        // Add color intensity
        float modifiedIntensity = currentIntensity + intensityChange;

        // Set color intensity
        float newScaleFactor = Mathf.Pow(2, modifiedIntensity);
        Color colorToRetun = originalColorWithoutIntensity * newScaleFactor;

        // Return color
        return colorToRetun;
    }
    #endregion
    #region Garbage
    //Default should not be used
    //public void setUpData(Color color, gridPlaceType placeType)
    //{
    //    this.color = color;
    //    this.placeType = placeType;
    //    setUpColor();
    //}
    ////Used when trying to spawn in new data_system
    //public void setUpData(Color color, gridPlaceType placeType, data_system systemData)
    //{
    //    this.color = color;
    //    this.emissiveColor = color;
    //    this.placeType = placeType;
    //    this.systemData = systemData;
    //    setUpColor();
    //}
#endregion


    #region Save/Load
    [System.Serializable]
    private struct SaveData
    {
        //Stellaris Info
        public gridPlaceType save_placeType;
        public data_system save_systemData;

        //Unity Info
        public saveColor save_color;
        public saveColor save_emissiveColor;
        public int save_emissiveNumber;
        public System.Guid save_UI_father;
        public saveVector3 save_position;

        //Unity Connections
        public List<System.Guid> save_connectedHyperlanes;
        public List<System.Guid> save_connectedNodes;

        //Used for ID
        public System.Guid save_ID;
    }

    public void loadState(object state)
    {
        SaveData saveData = (SaveData)state;
        this.placeType = saveData.save_placeType;
        this.systemData = saveData.save_systemData;
        this.color = saveData.save_color.getColor();
        this.emissiveColor = saveData.save_emissiveColor.getColor();
        this.emissiveNumber = saveData.save_emissiveNumber;
        this.UI_father = saveData.save_UI_father;

        this.connectedHyperlanes = new List<System.Guid>(saveData.save_connectedHyperlanes);
        this.connectedNodes = new List<System.Guid>(saveData.save_connectedNodes);
        this.gameObject.transform.position = saveData.save_position.getVector3();
        this.ID = saveData.save_ID;
        this.text_nameOfSystem.text = systemData.initializerName;

        setUpColor();

    }

    //Method called when saving data
    public object saveState()
    {
        return new SaveData()
        {
            save_placeType = this.placeType,
            save_systemData = this.systemData,
            save_color = new saveColor(this.color),
            save_emissiveColor = new saveColor(this.emissiveColor),
            save_emissiveNumber = this.emissiveNumber,
            save_UI_father = this.UI_father,

            save_connectedHyperlanes = new List<System.Guid>(this.connectedHyperlanes),
            save_connectedNodes = new List<System.Guid>(this.connectedNodes),
            save_position = new saveVector3(this.gameObject.transform.position),
            save_ID = this.ID,
        };
    }
    #endregion
}
