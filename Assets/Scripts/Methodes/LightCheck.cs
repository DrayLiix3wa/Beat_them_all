using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public class LightCheck : MonoBehaviour
{
    public SO_Door[] doors;

    private void Update()
    {
        CheckLevelCompleted();
    }

    public void CheckLevelCompleted()
    {
        foreach (var door in doors)
        {
            if(door.level.isWin)
            {
                door.isOpen = true;
            }
            else
            {
                door.isOpen = false;
            }
        }
    }
}
