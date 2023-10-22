using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MiddleObjects : MonoBehaviour
{
    public GameObject quitSection;
    public GameObject savedGalaxyPopUp;
    public void saveAndQuit()
    {
        SaveManager._SaveManager.save();
        idGenerator._idGenerator.save();
        Application.Quit();
    }
    public void quitAndDontSave()
    {
        idGenerator._idGenerator.save();
        Application.Quit();
    }
    public void cancel()
    {
        quitSection.SetActive(false);
    }
    public void trueQuit()
    {
        Application.Quit();
    }
    public void openQuit()
    {
        quitSection.SetActive(true);
    }
}
