using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPhaseData _phaseData;

    [Tooltip("敵の出現地点(扉の左右2箇所をGameObjectで指定)")]
    [SerializeField] GameObject[] _spawnPosObjs;
    Vector3 _spawnPos;

    EnemyPool _pool;

    int _currentPhase = 1;
    [Tooltip("敵の出現間隔")]
    float _spawnInterval;
    float timer = 0;

    bool _gameStart = false;

    private void ResetSpawner()
    {
        _pool = GetComponent<EnemyPool>();
        _pool.CreatePool();

        if (_phaseData == null || _pool == null || _spawnPosObjs == null)
        {
            Debug.LogError("<color=orange>必要なコンポーネントが未割当てです</color>");
            this.enabled = false;
            return;
        }

        _currentPhase = 1;
        _spawnInterval = _phaseData.GetSpawnInterval(_currentPhase);
        _phaseData.currentRate = 1f;
    }

    void Update()
    {
        if (_gameStart)
        {
            //時間経過でフェーズを切り替える
            timer += Time.deltaTime;

            if (timer > _phaseData.phaseLength)
            {
                _currentPhase++;
                timer = 0;
                int index = _currentPhase >= _phaseData.phase.Length ? 0 : _currentPhase;
                Debug.Log($"<color=green>フェーズ {_currentPhase - 1} -> {_currentPhase} \n 敵の出現間隔 {_phaseData.phase[index].intervalMin * _phaseData.currentRate} ～ {_phaseData.phase[index].intervalMax * _phaseData.currentRate} 秒</color>");
            }
        }
    }

    public void StartSpawner()
    {
        ResetSpawner();
        _gameStart = true;
        StartCoroutine(SpawnLoop());
    }

    /// <summary>
    /// 2か所からランダムで選んで敵を出現させる
    /// </summary>
    public void SpawnEnemy()
    {
        var direction = Random.Range(0, 2);
        _spawnPos = _spawnPosObjs[direction].transform.position;
        _pool.SpawnEnemy(_spawnPos, direction);
    }

    /// <summary>
    /// 一定間隔で敵を出現させる
    /// </summary>
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float newInterval = _spawnInterval;
            //Debug.Log($"敵の出現間隔 {newInterval} 秒");
            _spawnInterval = _phaseData.GetSpawnInterval(_currentPhase);
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnGameStart += StartSpawner;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= StartSpawner;
    }
}
