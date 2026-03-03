using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 敵のオブジェクトプール
/// </summary>
public class EnemyPool : MonoBehaviour
{
    [Tooltip("敵のPrefab")]
    [SerializeField] GameObject _enemyPrefab;
    [Tooltip("初期生成する敵の数")]
    [SerializeField] int _initialGeneration = 50;
    Queue<GameObject> _enemyPool = new Queue<GameObject>();

    private void Start()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("<color=orange>敵のPrefabが未割当てです</color>");
            this.enabled = false;
            return;
        }

        //最初に必要になる敵の数を生成する
        for (int i = 0; i < _initialGeneration; i++)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, this.transform.position, Quaternion.identity, this.transform);
            //[TODO] 敵の制御コンポーネントをdisable
            _enemyPool.Enqueue(newEnemy);
        }

    }

    /// <summary>
    /// 新たな敵を出現させる
    /// </summary>
    public void SpawnEnemy(Vector3 spawnPos)
    {
        if (_enemyPool.Count == 0)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab,this.transform.position, Quaternion.identity, this.transform);
            //[TODO] 敵の制御コンポーネントをdisable
            _enemyPool.Enqueue(newEnemy);
        }

        GameObject enemy = _enemyPool.Dequeue();
        enemy.transform.SetParent(null);
        enemy.GetComponent<SpriteRenderer>().enabled = true;
        //[TODO] 敵の制御コンポーネントをenable
        enemy.GetComponent<EnemyControllerTest>().enabled = true;
        enemy.transform.position = spawnPos;
    }

    /// <summary>
    /// 敵が死んだ時呼び出す
    /// </summary>
    /// <param name="enemy"></param>
    public void DespawnEnemy(GameObject enemy)
    {
        enemy.GetComponent<SpriteRenderer>().enabled = false;
        //[TODO] 敵の制御コンポーネントをdisable
        enemy.GetComponent<EnemyControllerTest>().enabled = false;
        enemy.transform.position = this.transform.position;
        enemy.transform.SetParent(this.transform);
        _enemyPool.Enqueue(enemy);
    }

}
