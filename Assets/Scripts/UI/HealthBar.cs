using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Dirk Dynamite/UI/HealthBar")]
[DisallowMultipleComponent]
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public SO_player player;

    void Start()
    {
        SetMaxHealth();
    }

    void Update()
    {
        SetHealth( player.health );
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
