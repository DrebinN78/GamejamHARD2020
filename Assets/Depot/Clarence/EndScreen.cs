using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public RectTransform policeWinPos;
    public RectTransform coroboyWinPos;

    public RectTransform resultPanel;

    public GameObject policeWinBar;
    public GameObject coroboyWinBar;

    //public Text coronaScore;
    //public Text policeScore;
    public Text results;
    float initialTimeScale;

    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button switchSelectedButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private float scaleValue = 1.2f;

    private void Awake()
    {
        initialTimeScale = Time.timeScale;
    }

    private void OnEnable()
    {
        playAgainButton.Select();

        //coronaScore.text = "Unmasked people: " + GameManager.unMaskedPeopleCounter;
        //policeScore.text = "Masked people: " + GameManager.maskedPeopleCounter;

        if (GameManager.unMaskedPeopleCounter > GameManager.maskedPeopleCounter)
        {
            results.text = "Coroboy wins !";
            resultPanel.position = coroboyWinPos.position;

            policeWinBar.SetActive(false);
            coroboyWinBar.SetActive(true);
        }
        else
        {
            results.text = "Corocop wins !";
            resultPanel.position = policeWinPos.position;

            policeWinBar.SetActive(true);
            coroboyWinBar.SetActive(false);
        }
        Time.timeScale = 0;
    }

    //void TweenButton(GameObject buttonGameObject)
    //{
    //    buttonGameObject.transform.DOScale(scaleValue, 1f)
    //        .OnComplete(() =>
    //        {
    //            buttonGameObject.transform.DOScale(1f, 1f)
    //            .OnComplete(() =>
    //            {
    //                TweenButton(buttonGameObject);
    //            });
    //        });
    //}


    public void PlayAgain()
    {

        Time.timeScale = initialTimeScale;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchCharacter()
    {
        Time.timeScale = initialTimeScale;
        SceneManager.LoadScene("RoleSelection");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = initialTimeScale;
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        Time.timeScale = initialTimeScale;
    }
}
