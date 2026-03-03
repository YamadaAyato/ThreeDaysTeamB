using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _interactRange;
    [SerializeField] private LayerMask _interactableLayer;
    /// <summary>
    ///     インタラクト処理を行う
    /// </summary>
    public bool TryInteract()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position, _interactRange, _interactableLayer);
        if(hit == null) return false;

    }
}
