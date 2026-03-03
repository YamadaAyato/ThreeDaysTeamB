using UnityEngine;

public class EnemyHitJudge : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = GetComponentInParent<Enemy>();

        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.AttackDamage(collision.gameObject);
        }
    }
}
