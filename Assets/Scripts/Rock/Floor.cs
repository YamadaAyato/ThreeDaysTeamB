using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField,Tooltip("反発力の倍率")] private float bounceMultiplier = 1.5f; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rock rock = collision.gameObject.GetComponent<Rock>();
        if (rock != null)
        {
            rock.StopFall(); // コルーチンによる落下を停止させる
            rock.canDamage = false; // ダメージを与えられないようにする
            Bounce(collision.gameObject);
        }
    }

    /// <summary>
    /// バウンド処理を行うメソッド
    /// </summary>
    private void Bounce(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 反発力を計算
            rb.constraints = RigidbodyConstraints2D.None;
            Vector2 bounceDirection = CalculationDirection();
            rb.AddForce(bounceDirection * bounceMultiplier, ForceMode2D.Impulse);
        }

        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if(collider != null)
        {
            collider.enabled = false; // コライダーを無効にして、再度衝突しないようにする
        }
    }

    /// <summary>
    /// 方向をランダムに計算するメソッド
    /// <para>yはプラス</para>
    /// </summary>
    /// <returns></returns>
    private Vector2 CalculationDirection()
    {
        float angle = Random.Range(0, 181);
        angle = angle * Mathf.Deg2Rad; // ラジアンに変換
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        return new Vector2(x,y);
    }
}
