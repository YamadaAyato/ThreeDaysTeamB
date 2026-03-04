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

    private Coroutine _scoreCoroutine;

    private void OnEnable()
    {
        GameEvents.OnGameStart += TimeStart;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= TimeStart;
    }

    private void TimeStart()
    {
        ScoreModel.Reset();

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
            ScoreModel.AddScore(interval);
            UpdateViews();
        }
    }
    //敵が死んだときようのもの　
    public void AddScore(int addScore)
    {
        ScoreModel.AddScore(addScore);
        _enemyCounter.AddEnemy();
        UpdateViews();
    }

    private void UpdateViews()
    {
        _view.UpdateScore(ScoreModel.Score);
        _rankingPresenter.UpdateRealtimeRanking(ScoreModel.Score);
    }
}