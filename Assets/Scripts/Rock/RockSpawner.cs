using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField,Tooltip("生成する岩")] private GameObject rockPrefab;
    [Tooltip("クールタイム")] public float coolTime = 5f; 
    [Tooltip("クールタイムのカウント")] public float timer = 0f;

    void Start()
    {
        SpawnRock();
    }

    /// <summary>
    /// 岩を生成するメソッド
    /// </summary>
    private void SpawnRock()
    {
        GameObject _rock = Instantiate(rockPrefab, transform.position, Quaternion.AngleAxis(Random.Range(0, 360), new Vector3(0, 0, 1)));
        _rock.GetComponent<Rock>().parent = this;
    }

    /// <summary>
    /// クールタイムを開始するコルーチン
    /// <para>クールタイムが終了すると岩を生成し、タイマーをリセットする</para>
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartCoolTime()
    {
        while (timer < coolTime)
        {
            timer += Time.deltaTime;
            yield return null; 
        }
        SpawnRock();
        timer = 0f;
    }
}
