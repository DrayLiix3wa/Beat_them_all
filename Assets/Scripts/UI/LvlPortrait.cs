using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlPortrait : MonoBehaviour
{
   
    public SO_Level level;
    public Image portrait;

    private void Start()
    {
        SetPortrait();
    }

    private void OnValidate()
    {
        SetPortrait();
    }

    public void SetPortrait()
    {
        if (level == null)
            return;

        portrait.sprite = level.levelPortrait;
    }
}
