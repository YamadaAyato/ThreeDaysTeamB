using System.Collections;
using UnityEngine;
/// <summary>
/// 時間の経過やイベントに応じてスコアを更新し、各所に伝える。
/// </summary>
public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private ScoreView _view;
    [SerializeField] private RankingPresenter _rankingPresenter;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private TimeView _timeView;
    private int _elapsedTime;//経過時間

    private Coroutine _scoreCoroutine;

    private void OnEnable()
    {
        GameEvents.OnGameStart += TimeStart;
        GameEvents.OnGameOver += OnGameOver;
        GameEvents.OnEnemyDefeated += AddEnemy;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= TimeStart;
        GameEvents.OnGameOver -= OnGameOver;
        GameEvents.OnEnemyDefeated -= AddEnemy;
    }

    private void TimeStart()
    {
        _elapsedTime = 0;
        _enemyCounter.Reset();
        if (_scoreCoroutine != null)
            StopCoroutine(_scoreCoroutine);

        _scoreCoroutine = StartCoroutine(ScoreLoop());

        UpdateViews();
    }

    private IEnumerator ScoreLoop()
    {
        const int interval = 1;

        while (true)
        {
            yield return new WaitForSeconds(interval);
            _elapsedTime += interval;
            UpdateViews();
        }
    }
    //敵が死んだときようのもの　
    public void AddEnemy()
    {
        _enemyCounter.AddEnemy();
        UpdateViews();
    }

    private void UpdateViews()
    {
        int realtimeScore = _elapsedTime * _enemyCounter.Count;
        _view.UpdateScore(realtimeScore);
        _timeView.UpdateTime(_elapsedTime);
        _rankingPresenter.UpdateRealtimeRanking(realtimeScore);
    }
    private void OnGameOver()
    {
        if (_scoreCoroutine != null)
        {
            StopCoroutine(_scoreCoroutine);
            _scoreCoroutine = null;
        }
        int finalScore = _elapsedTime * _enemyCounter.Count;
        PlayerPrefs.SetInt("MyElapsedTime", _elapsedTime);
        PlayerPrefs.SetInt("MyEnemyCount", _enemyCounter.Count);
        _rankingPresenter.RegisterFinalScore(finalScore);
    }
}