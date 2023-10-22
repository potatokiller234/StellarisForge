using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_nodeDataMod : MonoBehaviour
{
    public List<UI_nodeDataFile> UI_nodeDataFileHolder;
    TextMeshProUGUI UI_text;
    public string ModName;

    [SerializeField]
    private int disabledAmount = 0;


    public void disableChild(GameObject child)
    {
        child.SetActive(false);
        disabledAmount++;
        if (disabledAmount == UI_nodeDataFileHolder.Count)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void resetDisabled()
    {
        disabledAmount = 0;
    }


    public void UI_setNewNodeDataMod(string ModName, TextMeshProUGUI UI_text)
    {
        this.UI_text = UI_text;
        this.UI_text.text = ModName;
        UI_nodeDataFileHolder = new List<UI_nodeDataFile>();
        this.ModName = ModName;
    }

    public void UI_addNewFileNode(UI_nodeDataFile bob)
    {
        UI_nodeDataFileHolder.Add(bob);
    }

    private void OnDisable()
    {
        if (UI_nodeDataFileHolder == null)
            return;


        for (int i = 0; i < UI_nodeDataFileHolder.Count; i++)
        {
            UI_nodeDataFileHolder[i].gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (UI_nodeDataFileHolder == null)
            return;
        disabledAmount = 0;

        for (int i = 0; i < UI_nodeDataFileHolder.Count; i++)
        {
            UI_nodeDataFileHolder[i].gameObject.SetActive(true);
        }
    }
}
