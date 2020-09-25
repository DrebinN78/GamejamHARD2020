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

    [SerializeField] private AudioSource backSound;
    [SerializeField] private AudioSource clickSound;

    public void StartGame()
    {
        clickSound.Play();
        SceneManager.LoadScene("RoleSelection");
    }

    public void OpenTuto()
    {
        clickSound.Play();
        menu.SetActive(false);
        tutorial.SetActive(true);
        backTutoButton.Select();
    }

    public void OpenCredits()
    {
        clickSound.Play();
        menu.SetActive(false);
        credits.SetActive(true);
        backCreditsButton.Select();
    }

    public void BackToMenuScreen()
    {
        backSound.Play();
        menu.SetActive(true);

        tutorial.SetActive(false);
        credits.SetActive(false);

        startButton.Select();
    }

    public void BackFromTuto()
    {
        backSound.Play();
        menu.SetActive(true);

        tutorial.SetActive(false);
        credits.SetActive(false);

        tutoButton.Select();
    }

    public void BackFromCredits()
    {
        backSound.Play();
        menu.SetActive(true);

        tutorial.SetActive(false);
        credits.SetActive(false);

        creditsButton.Select();
    }

    public void ExitGame()
    {
        backSound.Play();
        Application.Quit();
    }
}
