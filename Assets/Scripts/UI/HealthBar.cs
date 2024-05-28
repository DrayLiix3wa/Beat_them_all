using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Dirk Dynamite/UI/HealthBar")]
[DisallowMultipleComponent]
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public TextMeshProUGUI textCurrent;
    public TextMeshProUGUI textMax;

    public SO_player player;

    void Start()
    {
        SetMaxHealth();
        textMax.text = player.maxHealth.ToString();
    }

    void Update()
    {
        SetHealth( player.health );
        textCurrent.text = player.health.ToString();
    }

    public void SetMaxHealth()
    {
        slider.maxValue = player.maxHealth;
        slider.value = player.health;

        fill.color = gradient.Evaluate( 1f );
    }

    public void SetHealth( int health )
    {
        slider.value = health;

        fill.color = gradient.Evaluate( slider.normalizedValue );
    }
}
