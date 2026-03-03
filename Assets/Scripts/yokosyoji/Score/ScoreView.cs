using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScore(float score)
    {
        scoreText.text="Score:" + score.ToString("00000.00");
    }
}
