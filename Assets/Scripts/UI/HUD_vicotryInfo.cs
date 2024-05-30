using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_vicotryInfo : MonoBehaviour
{
    enum VictoryType
    {
        KILLCOUNT,
        CHRONO
    }

    [SerializeField] 
    VictoryType victoryType;
    [SerializeField] 
    SO_Level level;

    public Sprite chronoIcon;
    public Sprite killCountIcon;

    public Image imageComponent;

    public TextMeshProUGUI victoryText;

    private void Start()
    {
        switch (victoryType)
        {
            case VictoryType.KILLCOUNT:
                imageComponent.sprite = killCountIcon;
                break;
            case VictoryType.CHRONO:
                imageComponent.sprite = chronoIcon;
                break;
        }
    }
    void Update()
    {
        switch (victoryType)
        {
            case VictoryType.KILLCOUNT:
                victoryText.text = "" + (level.killsToWin - level.killCount);

                break;
            case VictoryType.CHRONO:

                victoryText.text = "" + ConvertTime((level.timeToWin - level.chrono));

                break;
        }
        
    }

    public string ConvertTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
