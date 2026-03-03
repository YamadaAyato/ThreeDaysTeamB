using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "EnemyLevelData", menuName = "Scriptable Objects/EnemyLevelData")]
public class EnemyLevelData : ScriptableObject
{
    public int maxPhase = 9;
    [Tooltip("各フェーズの持続時間")]
    public float phaseLength = 10f;
    [Tooltip("エンドレスフェーズの加速率")]
    public float endlessPhaseRate = 0.99f;
    public float currentRate = 1f;
    [Tooltip("レベル毎の敵の出現条件。Element0はエンドレスフェーズの数値")]
    public Phase[] phase;

    /// <summary>
    /// 現在レベルの敵の出現間隔を取得。
    /// </summary>
    /// <param name="currentLevel"></param>
    /// <returns></returns>
    public float GetSpawnInterval(int currentLevel)
    {
        if (this.phase.Length <= currentLevel)
        {
            currentRate *= endlessPhaseRate;
            return Random.Range(this.phase[0].intervalMin * currentRate, this.phase[0].intervalMax * currentRate);
        }
        else
        {
            return Random.Range(this.phase[currentLevel].intervalMin, this.phase[currentLevel].intervalMax);
        }
    }
}

[Serializable]
public class Phase
{
    //public int enemyNum = 20;
    public float intervalMin = 1f;
    public float intervalMax = 2f;
}
