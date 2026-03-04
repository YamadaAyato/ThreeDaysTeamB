using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
///     タイトル状態での管理を行うクラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private PlayableDirector _playableDirector;

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
        _canvasGroup.gameObject.SetActive(false);
        _playableDirector.Play();
        Debug.Log("ゲームスタート");
    }
}
