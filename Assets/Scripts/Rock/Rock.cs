using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Rock : MonoBehaviour,IInteractable
{
    [SerializeField] private Rigidbody2D rb; 
    [Tooltip("この岩を生成したスポナー")] public RockSpawner parent;
    [Tooltip("敵にダメージを与えるか")] public bool canDamage = false;
    [SerializeField, Tooltip("速度")] private float speed = 5f;
    [SerializeField, Tooltip("色の変更時間")] private float colorChangeTime = 0.5f;
    [SerializeField, Tooltip("インタラクトされたときの色")] private Color interactColor = Color.red;
    private Coroutine fallCoroutine;
    private Quaternion rotation;

    void Start()
    {
        rotation = transform.rotation;
    }

    public void StartFall()
    {
        if (fallCoroutine != null)
        {
            StopFall();
        }
        fallCoroutine = StartCoroutine(Fall());
    }

    public void StopFall()
    {
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null;
        }
    }

    /// <summary>
    /// 落下処理を行うコルーチン
    /// </summary>
    private IEnumerator Fall()
    {
        parent.StartCoroutine(parent.StartCoolTime());
        canDamage = true;
        while (true)
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (canDamage && damageable != null)
        {
            damageable.Die();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void IInteractable.Interact(GameObject interactor)
    {
        this.GetComponent<SpriteRenderer>().DOColor(interactColor, colorChangeTime).OnComplete(() =>
        {
            StartFall();
        });
    }

    bool IInteractable.CanInteract(GameObject interactor)
    {
        return true;
    }
}
