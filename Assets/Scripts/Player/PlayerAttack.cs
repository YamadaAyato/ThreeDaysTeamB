using UnityEngine;

/// <summary>
///     プレイヤーの攻撃処理を行うクラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [Header("攻撃の設定")]
    [SerializeField, Tooltip("攻撃地点")] private Transform _origin;
    [SerializeField, Tooltip("攻撃できる範囲")] private float _radius;
    [SerializeField, Tooltip("攻撃できるレイヤー")] private LayerMask _layerMask;
    [SerializeField, Tooltip("敵に与えるダメージ")] private int _damage;

    /// <summary>
    ///     攻撃を試みる
    /// </summary>
    /// <returns></returns>
    public bool TryAttack()
    {
        Debug.Log("PlayerAttack: TryAttack called");

        // 攻撃範囲内に敵がいるかを判定する
        Collider2D[] hits = Physics2D.OverlapCircleAll(_origin.position, _radius, _layerMask);
        if (hits == null || hits.Length == 0) return false;

        Collider2D nearEnemy = null;
        float minDistance = float.MaxValue;

        // 攻撃範囲内に複数の敵がいる場合、最も近い敵を選択する
        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(this.transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearEnemy = hit;
            }
        }

        if (nearEnemy == null) return false;
        IDamageable damageable = nearEnemy.GetComponent<IDamageable>();

        if (damageable == null) return false;
        // 敵にダメージを与える
        damageable.TakeDamage(_damage);
        Debug.Log($"Enemy {nearEnemy.name} took {_damage} damage!");
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        // 攻撃範囲をシーンビューに表示する
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_origin.position, _radius);
    }
}
