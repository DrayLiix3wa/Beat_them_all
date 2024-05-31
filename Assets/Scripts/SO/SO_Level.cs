using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Dirk Dynamite/Level")]
public class SO_Level : ScriptableObject
{
    public string levelName;

    [Header("Level Infos for Panel Menu")]
    public string levelTitle;
    public string levelDescription;
    public Sprite levelPortrait;

    [Space(10)]
    public string sceneName;
    [Space(10)]
    public bool isWin = false;
    public bool isLoose = true;
    [Space(10)]
    public int timeToWin = 180;
    public int killsToWin = 30;
    [Space(10)]

    [Header("Stats")]
    public int killCount = 0;
    public int chrono = 0;

    public void KillCountAdd()
    {
        killCount++;
    }

    private void OnEnable()
    {
        killCount = 0;
        isWin = false;
        isLoose = true;
        chrono = 0;
    }
}
