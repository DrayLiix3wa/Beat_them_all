using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public SO_player player;
    public CanvasGroup canvas;
    public float duration = 1.5f;

    public float playerPercentDamage = 30f;

    private bool isFading = false;

    void Start()
    {
        canvas.alpha = 0;
    }

    void Update()
    {
        if (player.health <= player.maxHealth * playerPercentDamage / 100)
        {
            isFading = true;
            StartCoroutine(FadeEffect());
        }
        else
        {
            isFading = false;
        }
    }

    public void DamageIndicatorCanvas()
    {
        if (player.health <= player.maxHealth * playerPercentDamage / 100)
        {
            canvas.alpha = Mathf.Lerp(canvas.alpha, 1, Time.deltaTime);
        }
        else
        {
            canvas.alpha = Mathf.Lerp(canvas.alpha, 0, Time.deltaTime);
        }
    }

    IEnumerator FadeEffect()
    {
        while ( isFading )
        {
            // Fondu en entrée
            yield return FadeIn();
            // Fondu en sortie
            yield return FadeOut();
        }
    }

    IEnumerator FadeIn()
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(0, 1, counter / duration);
            yield return null; // Attendre jusqu'à la prochaine frame
        }
    }

    IEnumerator FadeOut()
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(1, 0, counter / duration);
            yield return null; // Attendre jusqu'à la prochaine frame
        }
    }
}