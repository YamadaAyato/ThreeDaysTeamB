using UnityEngine;

/// <summary>
///     プレイヤーの体力を管理するクラス
/// </summary>
public class PlayerHealth : MonoBehaviour,IDamageable
{
    [Header("Health")]
    [SerializeField, ReadOnly] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public void Die()
    {
        Debug.Log(" プレイヤー死亡 ");
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth < damage)
        {
            _currentHealth = 0;
        }
        else
        {
            _currentHealth -= damage;
        }
        Debug.Log($" プレイヤーに{damage}ダメージ、プレイヤーの現在のHP{_currentHealth}");
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
}
