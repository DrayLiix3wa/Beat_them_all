using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dirk Dynamite/LevelManager")]
[DisallowMultipleComponent]

public class LevelManager : MonoBehaviour
{
   public enum LevelGoal
    {
        KILLCOUNT, CHRONO
    }

    [Header("Objectifs")]
    public LevelGoal currentGoal;
    public float timeToWin = 180f;
    public float killsToWin = 30f;

    [Header("Stats")]
    public float chrono = 0f;

    public SO_Level StateLevel;
    public SO_player StatsPlayer;
    public float currentHealth;

    private void Update()
    {
        currentHealth = StatsPlayer.health;
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        switch (currentGoal)
        {
            case LevelGoal.KILLCOUNT:
                if (currentHealth == 0)
                {
                    StateLevel.isLoose = true;
                }
                else if(StateLevel.killCount >= killsToWin)
                {
                    StateLevel.isWin = true;
                }
                break;
            case LevelGoal.CHRONO:
                if (currentHealth == 0)
                {
                    StateLevel.isLoose = true;
                }
                else if (chrono >= timeToWin)
                {
                    StateLevel.isWin = true;
                }
                break;
            default:
                break;
        }
    }
}
