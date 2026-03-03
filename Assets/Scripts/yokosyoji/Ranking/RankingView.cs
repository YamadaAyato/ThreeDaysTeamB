using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// ランキングUI反映
/// </summary>
public class RankingView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankingText;
    public void UpdateRanking(List<(string name, int score)> list)
    {
        rankingText.text = "";

        for (int i = 0; i < list.Count; i++)
        {
            rankingText.text += $"{list[i].name} : {list[i].score:00000}\n";
        }
    }
}
