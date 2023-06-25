using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButtonScript : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveAndExit()
    {
        var deviceSettings = GetComponent<PanelEvents>().deviceSettings;
        deviceSettings.SaveSettings("device_settings.json");
    }
}
