using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    private int _enemyCount;
    public int Count => _enemyCount; 

    public void AddEnemy()
    {
        _enemyCount++;
        _countText.text = _enemyCount.ToString();
    }
    public void Reset()
    {
        _enemyCount = 0;
        _countText.text = "000000";
    }
}