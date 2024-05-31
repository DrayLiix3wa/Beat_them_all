using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LvlPanel : MonoBehaviour
{
    public SO_Level level;

    public TextMeshProUGUI levelTitle;
    public TextMeshProUGUI levelDescription;


    private void Start()
    {
        FillPanel();
    }

    private void OnValidate()
    {
        FillPanel();
    }

    public void FillPanel()
    {
        levelTitle.text = level.levelTitle;
        levelDescription.text = level.levelDescription;
    }
}
