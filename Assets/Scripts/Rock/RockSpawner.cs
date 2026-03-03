using System.Collections;
using TMPro;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField,Tooltip("クールタイム表示用のテキスト")] private TextMeshProUGUI coolTimeText;
    [SerializeField,Tooltip("生成する岩")] private GameObject rockPrefab;
    [Tooltip("クールタイム")] public float coolTime = 5f; 
    [Tooltip("クールタイムのカウント")] public float timer = 0f;

    void Start()
    {
        SpawnRock();
        coolTimeText.gameObject.SetActive(false);
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
        coolTimeText.gameObject.SetActive(true);
        while (timer < coolTime)
        {
            timer += Time.deltaTime;
            coolTimeText.text = $"{(coolTime - timer):F0}";
            yield return null; 
        }
        SpawnRock();
        timer = 0f;
        coolTimeText.gameObject.SetActive(false);
    }
}
