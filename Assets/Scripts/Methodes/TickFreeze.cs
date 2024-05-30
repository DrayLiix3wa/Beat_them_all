using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickFreeze : MonoBehaviour
{
    public bool isTimeScaleFreeze;
    private float timeToWait = 0.3f;
    public SpriteRenderer spriteRenderer;

    public void StartTickFreeze(float time)
    {
        timeToWait = time;
        StartCoroutine(TickFreezeCoroutine());
    }

    public IEnumerator TickFreezeCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        isTimeScaleFreeze = true;
        spriteRenderer.enabled = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(timeToWait);
        isTimeScaleFreeze = false;
        spriteRenderer.enabled = false;
        Time.timeScale = 1;
    }
}
