using TMPro;
using UnityEngine;

/// <summary>
///スコアUI反映
/// </summary>

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void UpdateScore(float score)
    {
        _scoreText.text= score.ToString("00000");
    }
}
