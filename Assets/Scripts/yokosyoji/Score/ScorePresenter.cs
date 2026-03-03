using TMPro;
using UnityEngine;

/// <summary> 計算をして伝えるところ

public class ScorePresenter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreView view;
    public ScoreModel model;

    private void Start()
    {
        model =new ScoreModel();
    }
    void Update()
    {
        model.AddSore(Time.deltaTime);
        view.UpdateScore(model._score);
    }

    //仮の敵が死んだときようのもの
    public void OnEnemyDefeated(int addScore)
    {
        model.AddSore(addScore);
        view.UpdateScore(model._score);
    }

}
