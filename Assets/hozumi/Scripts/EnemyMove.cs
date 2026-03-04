using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject[] point; //エネミーが折り返す場所
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;
    private int index = 0;
    private Enemy enemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        //反転処理
        if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            //エネミーのrotationをポイントのrotationに合わせる
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
        else if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
     }

    /// <summary>
    /// indexの初期化
    /// </summary>
    private void OnEnable()
    {
        index = 0;
        target = point[index].transform;
        if (enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }
        point[point.Length - 1] = enemy.Player;
    }
}
