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

    private void OnEnable()
    {
        GameEvents.OnGameOver += RegisterFinalScore;
        GameEvents.OnGameStart += CacheName;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= RegisterFinalScore;
        GameEvents.OnGameStart -= CacheName;
    }
    // GameOverで正式登録
    public void RegisterFinalScore()
    {
        _cachedPlayerName = PlayerPrefs.GetString("PlayerName", "NoName");
        _model.AddScore(_cachedPlayerName, ScoreModel.Score);
        int myRank = _model.GetRank(_cachedPlayerName, ScoreModel.Score);
        PlayerPrefs.SetInt("MyRank", myRank);
        PlayerPrefs.SetInt("MyFinalScore", ScoreModel.Score);
        PlayerPrefs.Save();
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


        if (displayList.Count > _model.MaxRank)
            displayList.RemoveRange(_model.MaxRank, displayList.Count - _model.MaxRank);

        _view.UpdateRanking(displayList);
    }
    private void CacheName()
    {
        _cachedPlayerName = PlayerPrefs.GetString("PlayerName", "NoName");
    }

}
