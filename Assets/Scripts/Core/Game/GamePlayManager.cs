using UnityEngine;
using System;

public enum GameState
{
    Roaming,
    Combat
}

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CurrentState = GameState.Roaming;
    }

    private void Start()
    {
        ChangeState(GameState.Roaming);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public void StartCombat()
    {
        ChangeState(GameState.Combat);
    }
}