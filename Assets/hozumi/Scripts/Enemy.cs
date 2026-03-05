using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [Header("エネミーステータス")]
    [SerializeField] public EnemyType EnemyType;
    [SerializeField] private int _maxEnemyHp = 10;
    [SerializeField] private int _enemyAttackDamage = 2;
    private int _currentEnemyHp;

    [Header("ノックバック設定")]
    [SerializeField] private float _knockbackDis = 2;
    [SerializeField] private float _knockbackTime = 0.5f; //ノックバックの持続時間
    [SerializeField] private bool _isKnockback = false;

    [Header("SE")]
    [SerializeField] private AudioClip _damageSE;

    public bool IsKnockback => _isKnockback;
    private AudioSource _audioSource;

    public GameObject Player { get; private set;}

    public void SetPlayer(GameObject player)
    {
        Player = player;
    }

    /// <summary>
    /// エネミーがダメージを受けたとき
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        _currentEnemyHp -= damage;

        _audioSource.PlayOneShot(_damageSE);

        StartCoroutine(KnockbackRoutine());

        if (_currentEnemyHp <= 0)
        {
            Die();
        }
    }

    public void KnockBack()
    {
        StartCoroutine(KnockbackRoutine());
    }

    /// <summary>
    /// エネミーがノックバックする処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator KnockbackRoutine()
    {
        _isKnockback = true;
        Debug.Log("ノックバック開始");
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

        //プレイヤーとエネミーの位置関係を計算して、ノックバックの方向を決定
        Vector2 knockbackDirection = (transform.position - Player.transform.position).normalized;

        //速度をゼロにしてからノックバックの力を加える
        rigidbody2D.linearVelocity = Vector2.zero;

        //ノックバックの力を加える
        rigidbody2D.AddForce(knockbackDirection * _knockbackDis, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_knockbackTime);
        rigidbody2D.linearVelocity = Vector2.zero; //ノックバック後の速度をリセット
        Debug.Log("ノックバック終了");
        _isKnockback = false;
    }

    public void Die()
    {
        Debug.Log("エネミー死亡");
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _currentEnemyHp = _maxEnemyHp;
    }

    /// <summary>
    /// エネミーがプレイヤーにぶつかったら攻撃する処理
    /// </summary>
    /// <param name="target"></param>
    public void AttackDamage(GameObject target)
    {
        target.GetComponent<IDamageable>().TakeDamage(_enemyAttackDamage);
    }    
}
