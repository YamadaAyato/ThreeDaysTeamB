using DG.Tweening;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;

    public void GameStartClick()
    {
        _canvasGroup.DOFade(0, _fadeDuration)
            .OnComplete(() 
            => StartGame()
            );
    }

    public void GameExitClick()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        //TODO : プレイヤーが召喚されるタイムラインを呼び出すとおもう
    }
}
