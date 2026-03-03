using UnityEngine;

/// <summary>
/// スコアを持つところ
/// </summary>
public static class ScoreModel
{
    public static int Score {  get; private set; }
    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="value"></param>
    public static void AddScore(int value)
    {
        Score += value;
    }
    /// <summary>
    ///　２回目のプレイ時にスコアが残らないようにリセット用
    /// </summary>
    public static void Reset()
    {
        Score = 0;
    }
}
