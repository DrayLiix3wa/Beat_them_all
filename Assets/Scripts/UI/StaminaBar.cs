using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Dirk Dynamite/UI/HealthBar")]
[DisallowMultipleComponent]
public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public TextMeshProUGUI textCurrent;
    public TextMeshProUGUI textMax;

    public SO_player player;

    void Start()
    {
        SetMaxStamina();
        textMax.text = player.maxStamina.ToString();
    }

    void Update()
    {
        SetStamina( player.stamina);
        textCurrent.text = player.stamina.ToString();
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
