using UnityEngine;

/// <summary>
///     プレイヤーの移動制御をするクラス
/// </summary>
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;

    /// <summary>
    ///     移動処理を行う
    /// </summary>
    /// <param name="xInput"></param>
    public void Move(float xInput)
    {
        _rb.linearVelocity = new Vector2(_moveSpeed * xInput, 0f);
    }

    /// <summary>
    ///     強制停止する
    /// </summary>
    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
}
