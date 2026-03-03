using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [Header("エネミーステータス")]
    [SerializeField] private int _maxEnemyHp = 10;
    [SerializeField] private float _enemyWalkSpeed = 3;
    [SerializeField] private int _enemyAttackDamage = 2;
    private int _currentEnemyHp;

    [Header("ノックバック設定")]
    [SerializeField] private float _knockbackDis = 5;
    [SerializeField] private float _knockbackSpeed = 3;

    /// <summary>
    /// エネミーがダメージを受けたとき
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        _currentEnemyHp -= damage;

        StartCoroutine(KnockbackRoutine());

        if (_currentEnemyHp <= 0)
        {
            Die();
        }
    }

    private IEnumerator KnockbackRoutine()
    {
        transform.position += new Vector3(-_knockbackDis, 0, 0);
        yield return new WaitForSeconds(2f);
    }

    public void Die()
    {
        Debug.Log("エネミー死亡");
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        _currentEnemyHp = _maxEnemyHp;
    }

    public void AttackDamage(GameObject target)
    {
        target.GetComponent<IDamageable>().TakeDamage(_enemyAttackDamage);
    }    
}
