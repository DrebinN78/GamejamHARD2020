using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    public MainMenu mainMenuScript;
    public bool isTuto = true;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (isTuto)
                mainMenuScript.BackFromTuto();
            else
                mainMenuScript.BackFromCredits();
        }
    }
}
