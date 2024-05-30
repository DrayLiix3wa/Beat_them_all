using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_LevelName : MonoBehaviour
{
    public TextMeshProUGUI levelName;
    public SO_Level level;

    void Start()
    {
        UpdateLevelName();
    }

    void Update()
    {
        UpdateLevelName();
    }

    public void UpdateLevelName()
    {
        if (level == null)
        {
            levelName.text = "No level";
            return;
        }
        levelName.text = level.levelName;
    }

    private void OnValidate()
    {
        UpdateLevelName();
    }
}
