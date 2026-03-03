using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [Tooltip("この岩を生成したスポナー")] public RockSpawner parent;
    [Tooltip("敵にダメージを与えるか")] public bool canDamage = false;
    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation; 
        Fall();
    }

    /// <summary>
    /// 落下処理を行うメソッド
    /// </summary>
    public void Fall()
    {
        parent.StartCoroutine(parent.StartCoolTime());
        canDamage = true;
        rb.simulated = true; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (canDamage && damageable != null)
        {
            damageable.Die();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
