using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Dirk Dynamite/UI/HealthBar")]
[DisallowMultipleComponent]
public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public SO_player player;

    void Start()
    {
        SetMaxStamina();
    }

    void Update()
    {
        SetStamina( player.stamina);
    }

    public void SetMaxStamina()
    {
        slider.maxValue = player.maxStamina;
        slider.value = player.stamina;

        fill.color = gradient.Evaluate( 1f );
    }

    public void SetStamina( int stamina )
    {
        slider.value = stamina;

        fill.color = gradient.Evaluate( slider.normalizedValue );
    }
}
