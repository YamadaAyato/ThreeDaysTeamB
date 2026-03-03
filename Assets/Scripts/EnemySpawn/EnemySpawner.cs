using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [Tooltip("敵の出現地点(扉の左右2箇所をGameObjectで指定)")]
    [SerializeField] GameObject[] _spawnPosObjs;
    [Tooltip("敵の出現間隔")]
    [SerializeField] float _spawnSpeed = 3f;
    EnemyPool _pool;
    Vector3 _spawnPos;

    void Start()
    {
        _pool = GetComponent<EnemyPool>();

        if (_pool == null || _spawnPosObjs == null)
        {
            Debug.LogError("<color=orange>必要なコンポーネントが未割当てです</color>");
            this.enabled = false;
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    /// <summary>
    /// 二か所からランダムで選んで敵を出現させる
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
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnSpeed);
        }
    }
}
