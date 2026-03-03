using UnityEngine;

/// <summary>
///     仮の敵クラス
/// </summary>
public class ExEnemy : MonoBehaviour,IDamageable
{
    [Header("Health")]
    [SerializeField,ReadOnly] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public void Die()
    {
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
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
}
