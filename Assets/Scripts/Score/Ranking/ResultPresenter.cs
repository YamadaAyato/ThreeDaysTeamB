using TMPro;
using UnityEngine;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _formulaText; // 時間×倒した数用

    private void Start()
    {
        int myRank = PlayerPrefs.GetInt("MyRank", -1);
        int myScore = PlayerPrefs.GetInt("MyFinalScore", 0);
        int myTime = PlayerPrefs.GetInt("MyElapsedTime", 0);
        int myCount = PlayerPrefs.GetInt("MyEnemyCount", 0);

        PlayerPrefs.DeleteKey("MyRank");
        PlayerPrefs.DeleteKey("MyFinalScore");
        PlayerPrefs.DeleteKey("MyElapsedTime");
        PlayerPrefs.DeleteKey("MyEnemyCount");
        PlayerPrefs.Save();

        _rankText.text = myRank == -1 ? "圏外" : $"{myRank}位";
        _scoreText.text = $"{myScore:00000}";
        _formulaText.text = $"{myTime}秒 × {myCount}体";
    }
}