using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action OnGameStart;

    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }
}
