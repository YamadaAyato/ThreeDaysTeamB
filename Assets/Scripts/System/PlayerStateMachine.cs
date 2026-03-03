using UnityEngine;

/// <summary>
///     プレイヤーの状態を管理するステート
/// </summary>
public enum PlayerState
{
    Idle,
    Move,
    ActionLocked,
}

/// <summary>
///     プレイヤーの状態を管理するステートマシン
/// </summary>
public class PlayerStateMachine
{
    /// <summary> 現在のプレイヤーの状態を示すプロパティ </summary>
    public PlayerState CurrentState => _currentState;

    /// <summary>  現在の状態がActionLockedかどうかを示すプロパティ </summary>
    public bool IsActionLocked => _currentState == PlayerState.ActionLocked;

    private PlayerState _currentState;

    /// <summary>
    ///     Stateを変更するメソッド
    /// </summary>
    /// <param name="newState"> 次のState </param>
    public void ChangeState(PlayerState newState)
    {
        if(_currentState == newState) return;
        _currentState = newState;
    }
}
