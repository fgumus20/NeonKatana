using UnityEngine;
using System;

public enum GameState
{
    Roaming,
    Planning,
    Attacking
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(GameState.Roaming);
    }
   
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Roaming:
                Time.timeScale = 1f;
                break;
            case GameState.Planning:
                Time.timeScale = 0f;
                break;
            case GameState.Attacking:
                Time.timeScale = 1f;
                break;
        }

        OnStateChanged?.Invoke(newState);
        Debug.Log($"Game State Changed: {newState}");
    }

    public void StartPlanningMode()
    {
        ChangeState(GameState.Planning);
    }
}