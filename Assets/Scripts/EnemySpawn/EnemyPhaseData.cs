using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "EnemyPhaseData", menuName = "Scriptable Objects/EnemyPhaseData")]
public class EnemyPhaseData : ScriptableObject
{
    //public int maxPhase = 9;
    [Header("各フェーズの持続時間")]
    [Tooltip("各フェーズの持続時間")]
    public float phaseLength = 10f;
    [Header("エンドレスフェーズの加速率")]
    [Tooltip("エンドレスフェーズの加速率")]
    public float endlessPhaseRate = 0.99f;
    [Header("現在の加速率")]
    [Tooltip("現在の加速率。ゲームの実行終了時に値が変わるが、再度実行するときに初期値(1f)に戻る")]
    public float currentRate = 1f;
    [Header("フェーズ毎の敵の出現間隔。Element0はエンドレスフェーズの数値")]
    [Tooltip("フェーズ毎の敵の出現間隔。Element0はエンドレスフェーズの数値")]
    public Phase[] phase;

    /// <summary>
    /// 現在レベルの敵の出現間隔を取得。
    /// </summary>
    /// <param name="currentPhase"></param>
    /// <returns></returns>
    public float GetSpawnInterval(int currentPhase)
    {
        if (this.phase.Length <= currentPhase)
        {
            currentRate *= endlessPhaseRate;
            return Random.Range(this.phase[0].intervalMin * currentRate, this.phase[0].intervalMax * currentRate);
        }
        else
        {
            return Random.Range(this.phase[currentPhase].intervalMin, this.phase[currentPhase].intervalMax);
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
