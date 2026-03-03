using UnityEngine;

/// <summary>
///     プレイヤーの入力管理を行うクラス
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInteractor _interactor;
    private PlayerAttack _attack;
    private PlayerStateMachine _playerStateMachine;

    private float _xInput;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _interactor = GetComponent<PlayerInteractor>();
        _attack = GetComponent<PlayerAttack>();
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

        // TODO : 左クリックでインタラクトを試みる
        if (leftInput)
        {

        }

        // 右クリックで攻撃を試みる
        if (rightInput)
        {
            if(_attack.TryAttack())
            {
                _playerMover?.Stop();
                _playerStateMachine.ChangeState(PlayerState.ActionLocked);
                return;
            }
        }

        // 左右の入力がある場合は移動状態に遷移し、入力がない場合は停止して待機状態に遷移する
        if (_playerMover != null)
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
