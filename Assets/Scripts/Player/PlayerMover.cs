using UnityEngine;

/// <summary>
///     プレイヤーの移動制御をするクラス
/// </summary>
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;
    private Animator _animator;

    /// <summary>
    ///     移動処理を行う
    /// </summary>
    /// <param name="xInput"></param>
    public void Move(float xInput)
    {
        _rb.linearVelocity = new Vector2(_moveSpeed * xInput, 0f);
        _animator.SetBool("IsWalk", true);
    }

    /// <summary>
    ///     強制停止する
    /// </summary>
    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
    }

    public void StopAnim()
    {
        _rb.linearVelocity = Vector2.zero;
        _animator.SetBool("IsWalk", false);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
}
