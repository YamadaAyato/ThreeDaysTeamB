using UnityEngine;

/// <summary>
///     プレイヤーの体力を管理するクラス
/// </summary>
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField, ReadOnly] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    private bool _isDead = false;
    public void Die()
    {
        _isDead = true;
        AudioManager.Instance.PlaySE("Death");
        GameEvents.GameOver();
        Debug.Log("プレイヤーが死亡しました。ゲームオーバー");
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        if (_currentHealth <= damage)
        {
            _currentHealth = 0;
            Die();
        }
        else
        {
            _currentHealth -= damage;
        }
        Debug.Log($" プレイヤーに{damage}ダメージ、プレイヤーの現在のHP{_currentHealth}");
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
}
