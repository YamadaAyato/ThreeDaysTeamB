using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action OnGameStart;
    public static event Action OnGameOver;

    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
