using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelと Viewを仲介し、表示用リストを加工する
/// </summary>

public class RankingPresenter : MonoBehaviour
{
    [SerializeField] private RankingView _view;
    private RankingModel _model;
    private string _cachedPlayerName;

    // GameOverで正式登録
    public void RegisterFinalScore()
    {
        _model.AddScore(_cachedPlayerName, ScoreModel.Score);
        SceneLoader.LoadScene("TestResult");
    }

    private void Awake()
    {
        _model = new RankingModel();
    }
    private void Start()
    {

        var tempList = new List<(string name, int score)>();
        foreach (var r in _model.Ranks)
        {
            tempList.Add((r.Name, r.Score));
        }
        _cachedPlayerName = PlayerPrefs.GetString("PlayerName", "NoName");
        _view.UpdateRanking(tempList);
    }

    public void UpdateRealtimeRanking(int currentScore)
    {

        List<(string name, int score)> displayList = new List<(string name, int score)>();

        foreach (var r in _model.Ranks)
        {
            displayList.Add((r.Name, r.Score));
        }

        // 自分の今のスコアを追加してソート
        displayList.Add((_cachedPlayerName, currentScore));
        displayList.Sort((a, b) => b.score - a.score);

        if (displayList.Count > RankingModel.MaxRank)
            displayList.RemoveRange(RankingModel.MaxRank, displayList.Count - RankingModel.MaxRank);

        _view.UpdateRanking(displayList);
    }

}
