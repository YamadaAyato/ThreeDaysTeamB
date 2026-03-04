using DG.Tweening;
using UnityEngine;

/// <summary>
///     タイトル状態での管理を行うクラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;

    /// <summary>
    ///     ゲーム開始ボタンがクリックされたときの処理
    /// </summary>
    public void GameStartClick()
    {
        _canvasGroup.DOFade(0, _fadeDuration)
            .OnComplete(() 
            => StartGame()
            );
    }

    /// <summary>
    ///     ゲーム終了ボタンがクリックされたときの処理
    /// </summary>
    public void GameExitClick()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        //TODO : プレイヤーが召喚されるタイムラインを呼び出すとおもう
    }
}
