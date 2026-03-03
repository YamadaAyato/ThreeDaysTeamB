using UnityEngine;

/// <summary>
/// スコアを持つところ
/// </summary>
public static class ScoreModel
{
    public static int Score {  get; private set; }
    
    public static void AddScore(int value)
    {
        Score += value;
    }
    public static void Reset()
    {
        Score = 0;
    }
}
