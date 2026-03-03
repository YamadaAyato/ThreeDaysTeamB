using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyLevelData _levelData;

    [Tooltip("敵の出現地点(扉の左右2箇所をGameObjectで指定)")]
    [SerializeField] GameObject[] _spawnPosObjs;
    Vector3 _spawnPos;

    EnemyPool _pool;

    int _currentPhase = 1;
    [Tooltip("敵の出現間隔")]
    float _spawnInterval;
    float timer = 0;

    void Start()
    {
        _pool = GetComponent<EnemyPool>();

        if (_levelData == null || _pool == null || _spawnPosObjs == null)
        {
            Debug.LogError("<color=orange>必要なコンポーネントが未割当てです</color>");
            this.enabled = false;
            return;
        }

        _currentPhase = 1;
        _spawnInterval = _levelData.GetSpawnInterval(_currentPhase);
        _levelData.currentRate = 1f;
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        //時間経過でフェーズを切り替える
        timer += Time.deltaTime;

        if (timer > _levelData.phaseLength)
        {
            _currentPhase++;
            timer = 0;
            int index = _currentPhase > _levelData.maxPhase ? 0 : _currentPhase;
            Debug.Log($"<color=green>フェーズ {_currentPhase-1} -> {_currentPhase} \n" +
                $"敵の出現間隔 {_levelData.phase[index].intervalMin * _levelData.currentRate} ～ {_levelData.phase[index].intervalMax * _levelData.currentRate} 秒</color>");
        }

    }

    /// <summary>
    /// 2か所からランダムで選んで敵を出現させる
    /// </summary>
    public void SpawnEnemy()
    {
        var direction = Random.Range(0, 2);
        _spawnPos = _spawnPosObjs[direction].transform.position;
        _pool.SpawnEnemy(_spawnPos);
    }

    /// <summary>
    /// 一定間隔で敵を出現させる
    /// </summary>
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float newInterval = _spawnInterval;
            Debug.Log($"敵の出現間隔 {newInterval} 秒");
            _spawnInterval = _levelData.GetSpawnInterval(_currentPhase);
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
