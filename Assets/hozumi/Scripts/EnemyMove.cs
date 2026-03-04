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
        if (Vector2.Distance(target.position, transform.position) < 0.1f && index < point.Length)
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
        if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.linearVelocity.x < 0)
        {   
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// indexの初期化
    /// </summary>
    public void OnEnable()
    {
        index = 0;
        target = point[index].transform;
        point[point.Length - 1] = enemy.Player;
    }
}
