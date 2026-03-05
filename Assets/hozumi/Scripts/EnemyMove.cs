using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject[] point; //エネミーが折り返す場所
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;
    private SpriteRenderer sr;
    private int index = 0;
    private Enemy enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
        target = point[index].transform;
    }

    /// <summary>
    /// エネミーがポイントに到達したら、次のポイントに移動する
    /// </summary>
    private void Update()
    {
        //ポイントに到達した && 次のポイントが存在する場合
        if (Vector2.Distance(target.position, transform.position) < 0.1f && index < point.Length - 1)
        {
            index++; //次のポイントに移動
            target = point[index].transform;
        }
    }

    /// <summary>
    /// エネミーがポイントに向かって移動する
    /// </summary>
    public void FixedUpdate()
    {
        if (enemy.IsKnockback) return; //ノックバック中は移動しない

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        //反転処理
        if (rb.linearVelocity.x < 0)
        {
            sr.flipX = false;
            //エネミーのrotationをポイントのrotationに合わせる
            transform.rotation = Quaternion.LookRotation(new Vector3(Vector3.forward.x, Vector3.forward.y, (target.position - transform.position).z));
        }
        else if (rb.linearVelocity.x > 0)
        {
            sr.flipX = true;
            transform.rotation = Quaternion.LookRotation(new Vector3(Vector3.forward.x, Vector3.forward.y, (target.position - transform.position).z));
        }
     }

    /// <summary>
    /// indexの初期化
    /// </summary>
    public void ResetEnemy(PlayerHealth playerHealth)
    {
        index = 0;
        target = point[index].transform;
        point[point.Length - 1] = playerHealth.gameObject;
    }
}
