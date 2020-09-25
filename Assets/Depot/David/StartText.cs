using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    private bool isFaded;
    private bool launchFading;
    [SerializeField]
    private float timeBeforeFading = 8f;
    [SerializeField]
    private float fadeDuration = 3f;

    float timer = 0;

    void Start()
    {

    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timeBeforeFading)
        {
            Fade();
        }
    }

    public void Fade()
    {
        CanvasGroup canvas = GetComponentInChildren<CanvasGroup>();

        StartCoroutine(DoFade(canvas, canvas.alpha, launchFading ? 1 : 0));

        launchFading = !launchFading;
    }

    IEnumerator DoFade(CanvasGroup canvas, float start, float end)
    {
        float counter = 0f;
        
        while(counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(start, end, counter / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
    }

}
