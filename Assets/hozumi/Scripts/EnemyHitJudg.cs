using UnityEngine;

public class EnemyHitJudg : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = GetComponentInParent<Enemy>();
        enemy.AttackDamage(collision.gameObject);
    }
}
