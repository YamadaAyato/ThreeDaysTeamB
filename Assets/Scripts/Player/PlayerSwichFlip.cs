using UnityEngine;

/// <summary>
///     プレイヤーの向きを切り替えるクラス
/// </summary>
public class PlayerSwichFlip : MonoBehaviour
{
    private SpriteRenderer _sprite;

    /// <summary>
    ///     向きを切り替えるメソッド
    /// </summary>
    /// <param name="xInput"></param>
    public void SwichFlip(float xInput)
    {
        // 画像が初期が左向きなため、右に移動する場合はtrueにする
        if (xInput > 0)
        {
            _sprite.flipX = true;
        }
        else if (xInput < 0)
        {
            _sprite.flipX = false;
        }
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
}
