using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_menus : MonoBehaviour
{
    public static UI_menus _UI_menus;

    [Header("Menus")]
    public GameObject colorPicker;
    public GameObject fileMenu;
    public GameObject generate;
    public GameObject view;
    public GameObject galaxy_info;

    [Header("User Info For Saves")]
    public GameObject savePopUp;

    //public GameObject tempTest;
    private void Awake()
    {
        //Debug.Log("Beans" + this.name);
        _UI_menus = this;
    }

    //private void Update()
    //{
    //    // Debug.Log(Input.mousePosition);
    //    if (Input.GetKey(KeyCode.F))
    //        setNewColorPicker(tempTest);
    //}

    public void UI_disableButton(GameObject button)
    {
        button.SetActive(false);
    }

    #region Files Button Methods
    public void loadOrNewGalaxy()
    {
        SaveManager._SaveManager.save();
        idGenerator._idGenerator.save();
        SceneManager.LoadScene(1);
    }
    public void save()
    {
        SaveManager._SaveManager.save();
        StartCoroutine(cleanRef._cleanRef.disableEnable(1f, savePopUp));
    }
    #endregion

    public void setPostion(GameObject newParent, GameObject thingToSetPos, bool ignoreMousePos)
    {
        #region disable other similar menus that are open
        List<GameObject> tempList = new List<GameObject>();
        tempList.Add(fileMenu);
        tempList.Add(generate);
        tempList.Add(view);
        tempList.Add(galaxy_info);

        for(int i = 0;i<tempList.Count;i++)
        {
            if (tempList[i] != thingToSetPos)
                UI_disableButton(tempList[i]);
        }

        #endregion


        thingToSetPos.gameObject.SetActive(true);
        RectTransform rt_setPos = thingToSetPos.GetComponent<RectTransform>();
        RectTransform rt_newParent = newParent.GetComponent<RectTransform>();

        rt_setPos.transform.SetParent(newParent.transform);
        rt_setPos.anchoredPosition = Vector2.zero;

        if(ignoreMousePos == false)
        {
            if (Input.mousePosition.x < (Screen.width / 2))
            {
                rt_setPos.anchoredPosition += new Vector2(rt_newParent.rect.width, 0);
            }
            //Mouse on right side
            else if (Input.mousePosition.x >= (Screen.width / 2))
            {
                rt_setPos.anchoredPosition -= new Vector2(rt_newParent.rect.width, 0);
            }
            //Invalid Pos
            else
            {
                Debug.LogError("Mouse in invalid pos " + Input.mousePosition);
            }
        }
        //rt_setPos.anchoredPosition -= new Vector2(0, (rt_setPos.rect.height / 2) - (rt_newParent.rect.height / 2));
        //rt_setPos.anchoredPosition -= new Vector2(0, (rt_setPos.rect.height / 2) - (rt_newParent.rect.height / 2));

    }



    public void setNewColorPicker(GameObject newParent)
    {
        RectTransform rt_colorPicker = colorPicker.GetComponent<RectTransform>();

        colorPicker.GetComponent<colorPicker>().isColorChoiceDone = false;
        colorPicker.gameObject.SetActive(true);
        RectTransform rt_newParent = newParent.GetComponent<RectTransform>();
        //  rt_colorPicker.anchorMin = rt_newParent.anchorMin;
        //  rt_colorPicker.anchorMax = rt_newParent.anchorMax;
        //  rt_colorPicker.anchoredPosition = rt_newParent.anchoredPosition;


        //  //Remove offset from parent
        //  rt_colorPicker.anchoredPosition += new Vector2(Screen.width - 100, 0);
        //  RectTransform TrueParent = this.GetComponent<RectTransform>();
        colorPicker.transform.parent = newParent.transform;
        rt_colorPicker.anchoredPosition = Vector2.zero;





      //  Debug.Log(rt_newParent.anchoredPosition);
      //  Debug.Log(rt_newParent.rect.position);
      //  Debug.Log(rt_newParent.localPosition);
      //  Debug.Log(rt_newParent.position);

      //  // rt_colorPicker.localPosition = rt_newParent.rect.position;

      //  float screenWidth = Screen.width;
      //  float mousePos = Input.mousePosition.x;
      ////  colorPicker.

        //Mouse on left side of screen
        if(Input.mousePosition.x < (Screen.width/2))
        {
            rt_colorPicker.anchoredPosition += new Vector2(rt_newParent.rect.width + 35, 0);
        }
        //Mouse on right side
        else if(Input.mousePosition.x >= (Screen.width / 2))
        {
            rt_colorPicker.anchoredPosition -= new Vector2(rt_newParent.rect.width + 35, 0);
        }
        //Invalid Pos
        else
        {
            Debug.LogError("Mouse in invalid pos " + Input.mousePosition);
        }
        rt_colorPicker.anchoredPosition -= new Vector2(0, (rt_colorPicker.rect.height / 2) - (rt_newParent.rect.height / 2));


        //Find out which side of screen button is on

        //If on left side then set the xPos of colorPicker to the width
        //If on right side then set xPos of colorPicker to colocker picker width
    }

    public void disableColorPicker()
    {
        colorPicker.SetActive(false);
    }
}
