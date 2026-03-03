using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ランキングのセーブロードができる
/// </summary>
public class RankingModel
{
    const int MaxRank = 5;

    public struct RankData
    {
        public string name;
        public int score;
    }

    List<RankData> ranks = new List<RankData>();
    public IReadOnlyList<RankData> Ranks => ranks;

    public RankingModel()
    {
        Load();
    }

    public void AddScore(string name, int score)
    {
        ranks.Add(new RankData { name = name, score = score });
        ranks.Sort((a, b) => b.score - a.score);

        if (ranks.Count > MaxRank)
            ranks.RemoveAt(ranks.Count - 1);

        Save();
    }

    void Save()
    {
        for (int i = 0; i < ranks.Count; i++)
        {
            PlayerPrefs.SetString($"RankName{i}", ranks[i].name);
            PlayerPrefs.SetInt($"RankScore{i}", ranks[i].score);
        }
        PlayerPrefs.Save();
    }

    void Load()
    {
        ranks.Clear();
        for (int i = 0; i < MaxRank; i++)
        {
            if (PlayerPrefs.HasKey($"RankScore{i}"))
            {
                RankData data = new RankData
                {
                    name = PlayerPrefs.GetString($"RankName{i}"),
                    score = PlayerPrefs.GetInt($"RankScore{i}")
                };
                ranks.Add(data);
            }
        }
    }
}