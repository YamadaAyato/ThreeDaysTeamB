using UnityEngine;

public class EnemyHitJudge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = GetComponentInParent<Enemy>();

        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.AttackDamage(collision.gameObject);
        }
    }
}
