using TMPro;
using UnityEngine;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        int myRank = PlayerPrefs.GetInt("MyRank", -1);
        int myScore = PlayerPrefs.GetInt("MyFinalScore", 0);

        PlayerPrefs.DeleteKey("MyRank");
        PlayerPrefs.DeleteKey("MyFinalScore");
        PlayerPrefs.Save();

        _rankText.text = myRank == -1 ? "圏外" : $"{myRank}位";
        _scoreText.text = $"{myScore:00000}";
    }
}