using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
/// <summary>
/// ランキングUI反映
/// </summary>
public class RankingView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _rankingText;
    private Vector2[] _initialPositions;

    public void Awake()
    {
        _initialPositions = new Vector2[_rankingText.Length];
        for (int i = 0; i < _rankingText.Length; i++)
        {
            _initialPositions[i] = _rankingText[i].rectTransform.anchoredPosition;
        }
    }
    public void UpdateRanking(List<(string name, int score)> list, List<(string name, int score)> lastList)
    {
        for (int i = 0; i < _rankingText.Length; i++)
        {
            if (i >= list.Count) { _rankingText[i].text = ""; continue; }

            var data = list[i];
            string newText = $"{data.name} : {data.score:00000}";
            if (i < lastList.Count && lastList[i] == data) continue;
            if (_rankingText[i].text != newText)
            {
                // アニメーションを呼ぶとき、その行の「元の位置」を渡す
                AnimateTextSlot(i, newText);
            }
        }
    }
    private void AnimateTextSlot(int index, string newString)
    {
        var textNode = _rankingText[index];
        Vector2 originPos = _initialPositions[index]; 

        textNode.DOKill();
        textNode.rectTransform.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(textNode.rectTransform.DOAnchorPosX(originPos.x + 10f, 0.1f))
           .Join(textNode.DOFade(0, 0.1f))
           .AppendCallback(() => textNode.text = newString)
           .Append(textNode.rectTransform.DOAnchorPosX(originPos.x - 3,0)) // 反対側へワープ
           .Append(textNode.rectTransform.DOAnchorPosX(originPos.x, 0.1f))   // 元の場所(originPos.x)に戻る
           .Join(textNode.DOFade(1, 0.25f));
    }
}
