using UnityEngine;

/// <summary>
///     プレイヤーの入力管理を行うクラス
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInteractor _interactor;
    private PlayerAttack _attack;
    private PlayerSwichFlip _swichFlip;
    private PlayerStateMachine _playerStateMachine;
    private Animator _animator;

    private float _xInput;

    /// <summary>
    ///     アニメーションが終わったときに呼び出される関数
    ///     プレイヤーの状態をActionLockedからIdleに遷移させる
    /// </summary>
    public void OnActionFinished()
    {
        _playerStateMachine.ChangeState(PlayerState.Idle);
    }

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _interactor = GetComponent<PlayerInteractor>();
        _attack = GetComponent<PlayerAttack>();
        _swichFlip = GetComponent<PlayerSwichFlip>();
        _playerStateMachine = new PlayerStateMachine();
        _animator = GetComponent<Animator>();

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        // プレイヤーの状態がActionLockedの場合、入力を処理しない
        if (_playerStateMachine.IsActionLocked)
        {
            Debug.Log("ActionLocked状態のため、入力を処理しません");
            return;
        }

        // 水平方向の入力を取得
        _xInput = Input.GetAxisRaw("Horizontal");

        bool leftInput = Input.GetMouseButtonDown(0);
        bool rightInput = Input.GetMouseButtonDown(1);

        // TODO : 左クリックでインタラクトを試みる
        if (leftInput)
        {
            Debug.Log("左クリック入力を検出");
            if (_interactor.TryInteract())
            {
                _playerMover?.Stop();
                _playerStateMachine.ChangeState(PlayerState.ActionLocked);
                return;
            }
        }

        // 右クリックで攻撃を試みる
        if (rightInput)
        {
            _animator.SetTrigger("Attack");
            _playerStateMachine.ChangeState(PlayerState.ActionLocked);
            _playerMover?.StopAnim();
            rightInput = false;
            return;
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
            _playerMover.StopAnim();
            return;
        }

        if (_xInput != 0f)
        {
            _playerMover.Move(_xInput);
            _swichFlip.SwichFlip(_xInput);
        }
        else
        {
            _playerMover.StopAnim();
        }
    }
}
