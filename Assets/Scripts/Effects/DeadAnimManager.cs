using UnityEngine;
using UnityEngine.Playables;

public class DeadAnimManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    private void PlayDeadAnimation()
    {
        _playableDirector.Play();
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += PlayDeadAnimation;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= PlayDeadAnimation;
    }
}