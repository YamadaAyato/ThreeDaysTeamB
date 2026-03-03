using System.Collections;
using TMPro;
using UnityEngine;

/// <summary> 
/// 時間の経過やイベントに応じてスコアを更新し、各所に伝える。
/// <summary> 
public class ScorePresenter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreView view;
    [SerializeField] private RankingPresenter rankingPresenter;
    private void Start()
    {
        StartCoroutine(ScoreLoop());
        ScoreModel.AddScore(0); // 秒として加算
        view.UpdateScore(ScoreModel.Score);

        rankingPresenter.UpdateRealtimeRanking((int)ScoreModel.Score);
    }
    IEnumerator ScoreLoop()
    {
        int interval =1; // 0.01秒ごとに処理する

        while (true)
        {
            yield return new WaitForSeconds(interval);

            ScoreModel.AddScore(interval);
            view.UpdateScore(ScoreModel.Score);

            rankingPresenter.UpdateRealtimeRanking((int)ScoreModel.Score);
        }
    }
    //敵が死んだときようのもの　
    public void OnEnemyDefeated(int addScore)
    {
        ScoreModel.AddScore(addScore);
        view.UpdateScore(ScoreModel.Score);
    }

}
