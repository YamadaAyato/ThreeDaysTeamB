using System.Collections;
using TMPro;
using UnityEngine;

/// <summary> 
/// 時間の経過やイベントに応じてスコアを更新し、各所に伝える。
/// <summary> 
public class ScorePresenter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ScoreView _view;
    [SerializeField] private RankingPresenter _rankingPresenter;
    private void Start()
    {
        ScoreModel.Reset();

        StartCoroutine(ScoreLoop());
        ScoreModel.AddScore(0);
        _view.UpdateScore(ScoreModel.Score);

        _rankingPresenter.UpdateRealtimeRanking((int)ScoreModel.Score);
    }
    IEnumerator ScoreLoop()
    {
        int interval =1;

        while (true)
        {
            yield return new WaitForSeconds(interval);

            ScoreModel.AddScore(interval);
            _view.UpdateScore(ScoreModel.Score);

            _rankingPresenter.UpdateRealtimeRanking((int)ScoreModel.Score);
        }
    }
    //敵が死んだときようのもの　
    public void OnEnemyDefeated(int addScore)
    {
        ScoreModel.AddScore(addScore);
        _view.UpdateScore(ScoreModel.Score);
    }

}
