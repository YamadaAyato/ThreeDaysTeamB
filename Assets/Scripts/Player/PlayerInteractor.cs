using UnityEngine;

/// <summary>
///     プレイヤーのインタラクト処理を行うクラス
/// </summary>
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _interactRange;
    [SerializeField] private LayerMask _interactableLayer;

    private Animator _animator;

    /// <summary>
    ///     インタラクト処理を行う
    /// </summary>
    public bool TryInteract()
    {
        // プレイヤーの周囲にインタラクト可能なオブジェクトがあるかを判定する
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position, _interactRange, _interactableLayer);
        if (hit == null) return false;

        // インタラクト可能なオブジェクトがあった場合、IInteractableインターフェースを持つかを確認する
        IInteractable interactable = hit.GetComponent<IInteractable>();
        if (interactable == null) return false;
        if (!interactable.CanInteract(this.gameObject)) return false;

        // インタラクト処理を実行する
        interactable.Interact(this.gameObject);

        //TODO: インタラクト成功時のアニメーションやSEを入れる
        _animator.SetTrigger("Interact");
        return true;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        // インタラクト範囲をシーンビューに表示する
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _interactRange);
    }
}
