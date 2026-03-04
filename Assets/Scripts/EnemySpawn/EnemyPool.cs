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

    private void Start()
    {
        if (_enemyPrefabs == null)
        {
            Debug.LogError("<color=orange>敵のPrefabが未割当てです</color>");
            this.enabled = false;
            return;
        }

        _enemyPool = new Dictionary<EnemyType, Queue<GameObject>>();

        //最初に必要になる敵の数を生成する
        foreach (var type in Enum.GetValues(typeof(EnemyType)))
        {
            _enemyPool[(EnemyType)type] = new Queue<GameObject>();

            for (int i = 0; i < _initialGeneration; i++)
            {
                GameObject newEnemy = Instantiate(_enemyPrefabs[(int)type], this.transform.position, Quaternion.identity, this.transform);
                newEnemy.GetComponent<SpriteRenderer>().enabled = false;
                //[TODO] 敵の制御コンポーネントをdisable
                _enemyPool[(EnemyType)type].Enqueue(newEnemy);
            }
        }

    }

    /// <summary>
    /// 新たな敵を出現させる
    /// </summary>
    public void SpawnEnemy(Vector3 spawnPos, int type)
    {
        if (_enemyPool[(EnemyType)type].Count == 0)
        {
            GameObject newEnemy = Instantiate(_enemyPrefabs[type],this.transform.position, Quaternion.identity, this.transform);
            //[TODO] 敵の制御コンポーネントをdisable
            _enemyPool[(EnemyType)type].Enqueue(newEnemy);
        }

        GameObject enemy = _enemyPool[(EnemyType)type].Dequeue();
        enemy.transform.SetParent(null);
        enemy.GetComponent<SpriteRenderer>().enabled = true;
        //[TODO] 敵の制御コンポーネントをenable
        //enemy.GetComponent<EnemyControllerTest>().enabled = true;
        enemy.transform.position = spawnPos;
    }

    /// <summary>
    /// 敵が死んだ時呼び出す
    /// </summary>
    /// <param name="enemy"></param>
    public void DespawnEnemy(GameObject enemy)
    {   
        var type = enemy.GetComponent<Enemy>().EnemyType;
        enemy.GetComponent<SpriteRenderer>().enabled = false;
        //[TODO] 敵の制御コンポーネントをdisable
        //enemy.GetComponent<EnemyControllerTest>().enabled = false;
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
