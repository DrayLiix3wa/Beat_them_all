using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnLevelFail = new UnityEvent();

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
                    OnLevelFail.Invoke();
                }
                else if(StateLevel.killCount >= killsToWin)
                {
                    StateLevel.isWin = true;
                    OnLevelFail.Invoke();
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
