using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 敵のオブジェクトプール
/// </summary>
public class EnemyPool : MonoBehaviour
{
    [Tooltip("敵のPrefab")]
    //[SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject[] _enemyPrefabs;
    [Tooltip("初期生成する敵の数")]
    [SerializeField] int _initialGeneration = 30;
    //Queue<GameObject>[] _enemyPool = new Queue<GameObject>[2];
    Dictionary<EnemyType, Queue<GameObject>> _enemyPool;

    GameObject _player;

    private void Start()
    {
        if (_enemyPrefabs == null)
        {
            Debug.LogError("<color=orange>敵のPrefabが未割当てです</color>");
            this.enabled = false;
            return;
        }

        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyPool = new Dictionary<EnemyType, Queue<GameObject>>();

        //最初に必要になる敵の数を生成する
        foreach (var type in Enum.GetValues(typeof(EnemyType)))
        {
            _enemyPool[(EnemyType)type] = new Queue<GameObject>();

            for (int i = 0; i < _initialGeneration; i++)
            {
                InstantiateEnemy((EnemyType)type);
            }
        }

    }

    void EnableComponents(GameObject enemy, bool enable)
    {
        enemy.GetComponent<SpriteRenderer>().enabled = enable;
        enemy.GetComponent<EnemyMove>().enabled = enable;
        enemy.GetComponent<Enemy>().enabled = enable;
        //enemy.GetComponent<EnemyControllerTest>().enabled = enable;
    }

    void InstantiateEnemy(EnemyType type)
    {
        GameObject newEnemy = Instantiate(_enemyPrefabs[(int)type], this.transform.position, Quaternion.identity, this.transform);
        EnableComponents(newEnemy, false);
        newEnemy.GetComponent<Enemy>().SetPlayer(_player);
        _enemyPool[(EnemyType)type].Enqueue(newEnemy);
    }

    /// <summary>
    /// 新たな敵を出現させる
    /// </summary>
    public void SpawnEnemy(Vector3 spawnPos, int type)
    {
        if (_enemyPool[(EnemyType)type].Count == 0)
        {
            InstantiateEnemy((EnemyType)type);
        }

        GameObject enemy = _enemyPool[(EnemyType)type].Dequeue();
        enemy.transform.SetParent(null);
        EnableComponents(enemy, true);
        enemy.transform.position = spawnPos;
    }

    /// <summary>
    /// 敵が死んだ時呼び出す
    /// </summary>
    /// <param name="enemy"></param>
    public void DespawnEnemy(GameObject enemy)
    {   
        var type = enemy.GetComponent<Enemy>().EnemyType;
        EnableComponents(enemy, false);
        enemy.transform.position = this.transform.position;
        enemy.transform.SetParent(this.transform);
        _enemyPool[type].Enqueue(enemy);
    }

}

public enum EnemyType
{
    Left = 0,
    Right = 1
}
