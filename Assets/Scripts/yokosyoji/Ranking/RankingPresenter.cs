using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ランキングの自分の追加ソートをする
/// </summary>

public class RankingPresenter : MonoBehaviour
{
    RankingModel model;
    [SerializeField] RankingView view;
    void Start()
    {
        model = new RankingModel();
        var tempList = new List<(string name, int score)>();
        foreach (var r in model.Ranks)
        {
            tempList.Add((r.name, r.score));
        }
        view.UpdateRanking(tempList);
    }

    public void UpdateRealtimeRanking(int currentScore)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "NoName");
        var tempList = new List<(string name, int score)>();

        // 既存ランキングをコピー
        foreach (var r in model.Ranks)
        {
            tempList.Add((r.name, r.score));
        }

        // 今の自分を追加
        tempList.Add((playerName, currentScore));

        // 並び替え
        tempList.Sort((a, b) => b.score - a.score);

        view.UpdateRanking(tempList);
    }

    // GameOverで正式登録
    public void RegisterFinalScore()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "NoName");
        model.AddScore(playerName,ScoreModel.Score);
        SceneLoader.LoadScene("Result");
    }
}
