using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameManager;

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

    [Header("Stats")]
    private float chrono = 0f;

    public SO_Level StateLevel;
    public SO_player StatsPlayer;

    public UnityEvent OnLevelFail = new UnityEvent();

    public GameManager _gameManager;

    [SerializeField]
    private bool _timerActive = false;
    private float currentHealth;


    private void Awake()
    {
        ResetLevel();
    }

    private void Update()
    {
        currentHealth = StatsPlayer.health;
        UpdateLevel();
        ManageChrono();
    }

    private void UpdateLevel()
    {
        if (currentHealth == 0)
        {
            StateLevel.isLoose = true;
            OnLevelFail.Invoke();
        }

        switch ( currentGoal )
        {
            case LevelGoal.KILLCOUNT:
                if (StateLevel.killCount >= StateLevel.killsToWin)
                {
                    StateLevel.isWin = true;
                }
                break;
            case LevelGoal.CHRONO:
                if (chrono >= StateLevel.timeToWin)
                {
                    StateLevel.isWin = true;
                }
                break;
            default:
                break;
        }
    }

    private void ManageChrono()
    {
        if ( _gameManager.currentState == GameState.PLAY && !_timerActive )
        {
            StartCoroutine( Chrono() );
            _timerActive = true;
        }
        else if (_gameManager.currentState != GameState.PLAY && _timerActive)
        {
            StopCoroutine(Chrono());
            _timerActive = false;
        }
    }

    private IEnumerator Chrono()
    {
        while (chrono <= StateLevel.timeToWin)
        {
            chrono += Time.deltaTime; 
            StateLevel.chrono = (int)chrono; 
            yield return null;
        }
    }

    public void ResetLevel()
    {
        StateLevel.chrono = 0;
        StateLevel.killCount = 0;
    }
}
