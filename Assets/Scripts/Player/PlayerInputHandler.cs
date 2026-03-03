using UnityEngine;

/// <summary>
///     プレイヤーの入力管理を行うクラス
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerStateMachine _playerStateMachine;

    private float _xInput;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerStateMachine = new PlayerStateMachine();
    }

    private void Update()
    {
        // プレイヤーの状態がActionLockedの場合、入力を処理しない
        if (_playerStateMachine.IsActionLocked) return;

        // 水平方向の入力を取得
        _xInput = Input.GetAxisRaw("Horizontal");

        bool leftInput = Input.GetMouseButtonDown(0);
        bool rightInput = Input.GetMouseButtonDown(1);

        if(_playerMover != null)
        {
            if (_xInput != 0f)
            {
                _playerStateMachine.ChangeState(PlayerState.Move);
            }
            else
            {
                _playerMover.Stop();
                _playerStateMachine.ChangeState(PlayerState.Idle);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_playerStateMachine.IsActionLocked)
        {
            _playerMover.Stop();
            return;
        }

        if (_xInput != 0f)
            _playerMover.Move(_xInput);
        else
            _playerMover.Stop();
    }
}
