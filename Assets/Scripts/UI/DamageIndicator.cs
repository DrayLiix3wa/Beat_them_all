using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageIndicator : MonoBehaviour
{
    public SO_player player;
    public CanvasGroup canvas;

    public float playerPercentDamage = 30f;

    void Start()
    {
        canvas.alpha = 0;
    }

    void Update()
    {
        DamageIndicatorCanvas();
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
}
