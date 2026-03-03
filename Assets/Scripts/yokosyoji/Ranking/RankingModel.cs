using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ランキングのソートをしてセーブロードを行う
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
            if (i < ranks.Count)
            {
                // リストにデータがある順位は、新しい内容で上書き保存
                PlayerPrefs.SetString($"RankName{i}", ranks[i].name);
                PlayerPrefs.SetInt($"RankScore{i}", ranks[i].score);
            }
            else
            {
                // スコアが少なくてリストから溢れた（切れた）順位は、
                // 古いデータが残らないようにPlayerPrefsから消去する
                PlayerPrefs.DeleteKey($"RankName{i}");
                PlayerPrefs.DeleteKey($"RankScore{i}");
            }
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