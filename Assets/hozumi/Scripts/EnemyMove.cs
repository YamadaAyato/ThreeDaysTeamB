using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject[] point; //エネミーが折り返す場所
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;
    private int index = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = point[index].transform;
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) < 0.1f)
        {
            index++; //次のポイントに移動
            target = point[index].transform;
        }
    }

    public void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        //if (Vector2.Distance(target.position, transform.position) < 0.1f)
        //{
        //    Update();
        //}

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

    public void FollowPlayer()
    {
        //プレイヤーを追いかける処理
        if (point[3])
        {

        }
    }
}
