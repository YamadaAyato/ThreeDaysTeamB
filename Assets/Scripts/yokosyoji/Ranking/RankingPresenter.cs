using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modelと Viewを仲介し、表示用リストを加工する
/// </summary>

public class RankingPresenter : MonoBehaviour
{
    [SerializeField] RankingView view;
    RankingModel model;
    private string cachedPlayerName;

    private void Awake()
    {
        model = new RankingModel();
    }
    void Start()
    {

        var tempList = new List<(string name, int score)>();
        foreach (var r in model.Ranks)
        {
            tempList.Add((r.name, r.score));
        }
        cachedPlayerName = PlayerPrefs.GetString("PlayerName", "NoName");
        view.UpdateRanking(tempList);
    }

    public void UpdateRealtimeRanking(int currentScore)
    {

        List<(string name, int score)> displayList = new List<(string name, int score)>();

        foreach (var r in model.Ranks)
        {
            displayList.Add((r.name, r.score));
        }

        // 自分の今のスコアを追加してソート
        displayList.Add((cachedPlayerName, currentScore));
        displayList.Sort((a, b) => b.score - a.score);

        if (displayList.Count > 5)
            displayList.RemoveRange(5, displayList.Count - 5);

        view.UpdateRanking(displayList);
    }

    // GameOverで正式登録
    public void RegisterFinalScore()
    {
        model.AddScore(cachedPlayerName, ScoreModel.Score);
        SceneLoader.LoadScene("TestResult");
    }
}
