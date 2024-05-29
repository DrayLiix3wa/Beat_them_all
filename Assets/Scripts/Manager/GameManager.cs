using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[AddComponentMenu("Dirk Dynamite/GameManager")]
[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        START,
        PLAY,
        PAUSE,
        END,
        WIN
    }

    [Header("Game State")]
    public GameState currentState;

    public bool isStart = false;
    public bool isPlay = false;
    public bool isPause = false;
    public bool isEnd = false;

    [Space(10)]

    [Header("Events")]
    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onPauseEnter = new UnityEvent();
    public UnityEvent onPauseExit = new UnityEvent();
    public UnityEvent onEnd = new UnityEvent();
    public UnityEvent onWin = new UnityEvent();

    public SO_Level StateLevel;

    #region Unity Lifecycle

    private void Awake()
    {
        TransitionToState(GameState.START);
    }

    private void Update()
    {
        OnStateUpdate();
    }
    #endregion

    #region States
    private void OnStateEnter()
    {
        switch (currentState)
        {
            case GameState.START:
                onStart.Invoke();
                Time.timeScale = 0;
                isStart = true;
                break;
            case GameState.PLAY:
                Time.timeScale = 1;
                onPlay.Invoke();
                isPlay = true;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0;
                onPauseEnter.Invoke();
                isPause = true;
                break;
            case GameState.END:
                Time.timeScale = 0;
                if (StateLevel.isWin)
                {
                    onWin.Invoke();
                }
                else
                {
                    onEnd.Invoke();
                }
                isEnd = true;
                break;
            default:
                break;
        }
    }


    private void OnStateUpdate()
    {
        switch (currentState)
        {
            case GameState.START:
                if (isPlay)
                {
                    TransitionToState(GameState.PLAY);
                }
                else if (isPause)
                {
                    TransitionToState(GameState.PAUSE);
                }
                else if (isEnd || StateLevel.isWin)
                {
                    TransitionToState(GameState.END);
                }
                break;
            case GameState.PLAY:
                if (isStart)
                {
                    TransitionToState(GameState.START);
                }
                else if (isPause)
                {
                    TransitionToState(GameState.PAUSE);
                }
                else if (isEnd || StateLevel.isWin)
                {
                    TransitionToState(GameState.END);
                }
                break;
            case GameState.PAUSE:
                if (isStart)
                {
                    TransitionToState(GameState.START);
                }
                else if (isPlay)
                {
                    TransitionToState(GameState.PLAY);
                }
                else if (isEnd || StateLevel.isWin)
                {
                    TransitionToState(GameState.END);
                }
                break;
            case GameState.END:
                if (isStart)
                {
                    TransitionToState(GameState.START);
                }
                else if (isPlay)
                {
                    TransitionToState(GameState.PLAY);
                }
                else if (isPause)
                {
                    TransitionToState(GameState.PAUSE);
                }
                break;
            default:
                break;
        }
    }


    private void OnStateExit()
    {
        switch (currentState)
        {
            case GameState.START:
                isStart = false;
                break;
            case GameState.PLAY:
                isPlay = false;
                break;
            case GameState.PAUSE:
                onPauseExit.Invoke();
                Time.timeScale = 1;
                isPause = false;
                break;
            case GameState.END:
                isStart = false;
                break;
            default:
                break;
        }
    }

    private void TransitionToState(GameState state)
    {
        OnStateExit();
        currentState = state;
        OnStateEnter();
    }
    #endregion

    #region Public Methods

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        isPlay = true;
    }

    public void PauseGame()
    {
        isPause = true;
    }

    public void ResumeGame()
    {
        isPlay = true;
    }
    #endregion

    public void GameOver()
    {
        isEnd = true;
    }
    
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Pause(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if ( isPlay )
                {
                    PauseGame();
                }
                else if ( isPause )
                {
                    ResumeGame();
                }
                break;
            case InputActionPhase.Canceled:
                    break;
            default:
                break;
        }
    }
}
