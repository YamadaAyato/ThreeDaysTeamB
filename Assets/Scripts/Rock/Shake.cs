using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    [SerializeField, Tooltip("揺れの持続時間")] private float shakeDuration = 0.5f;
    [SerializeField, Tooltip("揺れの強さ")] private float shakeStrength = 0.5f;
    [SerializeField, Tooltip("振動数")] private int vibrato = 10;
    [SerializeField, Tooltip("手振れ値")] private float randomness = 90f;
    [SerializeField, Tooltip("スナップするか")] private bool snapping = false;
    [SerializeField, Tooltip("振動の減衰")] private bool fadeOut = true;

    /// <summary>
    /// 振動を開始するメソッド
    /// </summary>
    public void PlayShake()
    {
        transform.DOComplete(); // 既存のアニメーションを完了させる
        transform.DOShakePosition(shakeDuration, shakeStrength, vibrato, randomness, snapping, fadeOut);// 揺らす
    }
}
