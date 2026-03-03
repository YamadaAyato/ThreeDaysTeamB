using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// ランキングUI反映
/// </summary>
public class RankingView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankingText;
    public void UpdateRanking(List<(string name, int score)> list)
    {
        _rankingText.text = "";

        for (int i = 0; i < list.Count; i++)
        {
            _rankingText.text += $"{list[i].name} : {list[i].score:00000}\n";
        }
    }
}
