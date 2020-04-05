using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuUItesting : MonoBehaviour
{
    public Toggle[] qualitySettings;

    public void startGame()
    {
        foreach(Toggle t in qualitySettings)
        {
            if(t.isOn == true)
            {
                staticScript.pcOptomaization(t.transform.name.ToString().Trim());
            }
        }
        SceneManager.LoadScene("SampleScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
