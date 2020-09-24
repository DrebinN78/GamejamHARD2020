using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject credits;

    [SerializeField] private Button startButton;
    [SerializeField] private Button tutoButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backTutoButton;
    [SerializeField] private Button backCreditsButton;

    public void StartGame()
    {
        SceneManager.LoadScene("RoleSelection");
    }

    public void OpenTuto()
    {
        menu.SetActive(false);
        tutorial.SetActive(true);
        backTutoButton.Select();
    }

    public void OpenCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
        backCreditsButton.Select();
    }

    public void BackToMenuScreen()
    {
        menu.SetActive(true);

        tutorial.SetActive(false);
        credits.SetActive(false);

        startButton.Select();
    }

    public void BackFromTuto()
    {
        menu.SetActive(true);

        tutorial.SetActive(false);
        credits.SetActive(false);

        tutoButton.Select();
    }

    public void BackFromCredits()
    {
        menu.SetActive(true);

        tutorial.SetActive(false);
        credits.SetActive(false);

        creditsButton.Select();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
