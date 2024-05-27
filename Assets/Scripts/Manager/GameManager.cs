using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dirk Dynamite/GameManager")]
[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        START,
        PLAY,
        PAUSE,
        END
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


    #region Unity Lifecycle
    private void Start()
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
                isStart = true;
                break;
            case GameState.PLAY:
                onPlay.Invoke();
                isPlay = true;
                break;
            case GameState.PAUSE:
                onPauseEnter.Invoke();
                isPause = true;
                break;
            case GameState.END:
                onEnd.Invoke();
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
                else if (isEnd)
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
                else if (isEnd)
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
                else if (isEnd)
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
    #endregion

}
