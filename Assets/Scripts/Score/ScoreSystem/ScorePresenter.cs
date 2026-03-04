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
    private int _elapsedTime;//経過時間

    private Coroutine _scoreCoroutine;

    private void OnEnable()
    {
        GameEvents.OnGameStart += TimeStart;
        GameEvents.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= TimeStart;
        GameEvents.OnGameOver -= OnGameOver;
    }

    private void TimeStart()
    {
        _elapsedTime = 0;
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
    public void AddScore(int addScore)
    {
        _enemyCounter.AddEnemy();
        UpdateViews();
    }

    private void UpdateViews()
    {
        //int realtimeScore = _elapsedTime * _enemyCounter.Count;
        int realtimeScore = _elapsedTime * 5;//デバッグ用
        _view.UpdateScore(realtimeScore);
        _rankingPresenter.UpdateRealtimeRanking(realtimeScore);
    }
    private void OnGameOver()
    {
        int finalScore = _elapsedTime * _enemyCounter.Count;
        PlayerPrefs.SetInt("MyElapsedTime", _elapsedTime);
        PlayerPrefs.SetInt("MyEnemyCount", _enemyCounter.Count);
        _rankingPresenter.RegisterFinalScore(finalScore);
    }
}