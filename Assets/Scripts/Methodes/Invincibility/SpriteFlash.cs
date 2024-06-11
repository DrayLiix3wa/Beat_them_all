using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    private Color startColor;

    public void Awake()
    {
        startColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _spriteRenderer.color = startColor;
    }

    public void StartSpriteFlash(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(FlashCoroutine(flashDuration, flashColor, numberOfFlashes));
    }

    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        //Color startColor = _spriteRenderer.color;
        float elapsedFlashTime = 0;
        float elapsedFlashPercentage = 0;

        while (elapsedFlashTime < flashDuration)
        {
            elapsedFlashTime += Time.deltaTime;
            elapsedFlashPercentage = elapsedFlashTime / flashDuration;

            if (elapsedFlashPercentage > 1)
            {
                elapsedFlashPercentage = 1;
            }

            float pingPongPourcentage = Mathf.PingPong(elapsedFlashPercentage * 2 * numberOfFlashes, 1);
            _spriteRenderer.color = Color.Lerp(startColor, flashColor, pingPongPourcentage);

            yield return null;
        }

        _spriteRenderer.color = startColor;
    }
}
