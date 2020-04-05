using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;using UnityEngine.SceneManagement;

public class genrateFromButtonSCript : MonoBehaviour
{
    public TMP_InputField inputFeildX;
    public TMP_InputField inputFeildY;

    public void Generate()
    {
        inputFeildX.GetComponent<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.Decimal;
        inputFeildY.GetComponent<TMP_InputField>().characterValidation = TMP_InputField.CharacterValidation.Decimal;

        float xP = (float)double.Parse(inputFeildX.text.ToString());
        float yP = (float)double.Parse(inputFeildY.text.ToString());

        staticScript.xPower = xP;
        staticScript.yPower = yP;

        SceneManager.LoadScene("SampleScene 1");
    }
}
