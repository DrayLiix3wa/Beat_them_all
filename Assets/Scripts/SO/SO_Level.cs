using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Dirk Dynamite/Level")]
public class SO_Level : ScriptableObject
{
    public string levelName;

    public bool isWin = false;
    public bool isLoose = true;

    public int killCount = 0;

    public void KillCountAdd()
    {
        killCount++;
    }

    private void OnEnable()
    {
        killCount = 0;
        isWin = false;
        isLoose = true;
    }
}
