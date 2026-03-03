using UnityEngine;

/// <summary>
///     インタラクト可能なオブジェクトが実装するインターフェース
/// </summary>
public interface IInteractable
{
    /// <summary> インタラクトができるかを判定する </summary>
    bool CanInteract(GameObject interactor);

    /// <summary> インタラクト処理を行う </summary>
    void Interact(GameObject interactor);
}
