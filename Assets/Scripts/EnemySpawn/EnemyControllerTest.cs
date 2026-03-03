using System.Collections;
using UnityEngine;

public class EnemyControllerTest : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
    [SerializeField] float _lifeTime = 5f;
    EnemyPool _pool;

    void OnEnable()
    {
        var obj = GameObject.Find("EnemySpawner");
        _pool = obj.GetComponent<EnemyPool>();

        if (_pool == null)
        {
            Debug.Log("pool is empty");
        }

        Invoke(nameof(DespawnSelf), _lifeTime);
    }

    void Update()
    {
        var newPos = transform.position + new Vector3(0, _speed, 0);
        transform.position = newPos;

    }

    void DespawnSelf()
    {
        _pool.DespawnEnemy(this.gameObject);
    }
}
