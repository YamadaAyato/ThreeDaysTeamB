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
        public string Name;
        public int Score;
    }
    private List<RankData> _ranks = new List<RankData>();

    public IReadOnlyList<RankData> Ranks => _ranks;

    public RankingModel()
    {
        Load();
    }

    public void AddScore(string name, int score)
    {
        _ranks.Add(new RankData { Name = name, Score = score });
        _ranks.Sort((a, b) => b.Score - a.Score);

        if (_ranks.Count > MaxRank)
            _ranks.RemoveAt(_ranks.Count - 1);

        Save();
    }

    void Save()
    {
        for (int i = 0; i < _ranks.Count; i++)
        {
            if (i < _ranks.Count)
            {
                // リストにデータがある順位は、新しい内容で上書き保存
                PlayerPrefs.SetString($"RankName{i}", _ranks[i].Name);
                PlayerPrefs.SetInt($"RankScore{i}", _ranks[i].Score);
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
        _ranks.Clear();
        for (int i = 0; i < MaxRank; i++)
        {
            if (PlayerPrefs.HasKey($"RankScore{i}"))
            {
                RankData data = new RankData
                {
                    Name = PlayerPrefs.GetString($"RankName{i}"),
                    Score = PlayerPrefs.GetInt($"RankScore{i}")
                };
                _ranks.Add(data);
            }
        }
    }
}