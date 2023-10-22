using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_solarSystem : MonoBehaviour
{
    public solarSystemSpawn _solarSystemSpawn;
    public GameObject trueParent;
    public GameObject prefab_textMeshProText;


    GameObject button;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            button = Instantiate(prefab_textMeshProText);
            button.transform.SetParent(trueParent.transform);
            button.GetComponent<TextMeshProUGUI>().text = "FEET ARE GOOD";
            button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }


}
