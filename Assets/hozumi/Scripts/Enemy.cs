using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("エネミーステータス")]
    [SerializeField] private int _maxEnemyHp = 10;
    [SerializeField] private float _enemyWalkSpeed = 3;
    private int _currentEnemyHp;

    [Header("ノックバック設定")]
    [SerializeField] private float _knockbackDis = 5;
    [SerializeField] private float _knockbackSpeed = 3;

    [Header("武器の種類")]
    [SerializeField] public int _stoneDamege; //岩
    [SerializeField] public int _muchiSpeed; //鞭

    [Header("スポーン場所")]
    [SerializeField] public GameObject _spawnerLeft;
    [SerializeField] public GameObject _spawnerRight;

    /// <summary>
    /// エネミーがダメージを受けたとき
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        _currentEnemyHp -= damage;

        StartCoroutine(KnockbackRoutine());

        if (_currentEnemyHp <= 0)
        {
            Die();
        }
    }

    private IEnumerator KnockbackRoutine()
    {
        transform.position += new Vector3(-_knockbackDis, 0, 0);
        yield return new WaitForSeconds(2f);
    }

    public void Die()
    {
        Debug.Log("エネミー死亡");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 死んだ後にステータスリセット
    /// </summary>
    public void OnEnable()
    {
        _currentEnemyHp = _maxEnemyHp;
        transform.position = Vector3.zero;
    }
}
