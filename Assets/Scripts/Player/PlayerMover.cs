using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;

    public void Move(float xInput)
    {
        _rb.linearVelocity = new Vector2(_moveSpeed * xInput, 0f);
    }

    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
}
