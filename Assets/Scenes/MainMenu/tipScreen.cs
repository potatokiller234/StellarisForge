using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipScreen : MonoBehaviour
{
    public GameObject tipMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (tipMenu.activeSelf == true)
                tipMenu.SetActive(false);
            else
                tipMenu.SetActive(true);
        }
    }
}
