using TMPro;
using UnityEngine;
/// <summary>
/// 経過時間UI反映
/// </summary>
public class TimeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    public void UpdateTime(int time)
    {
        _timeText.text = time.ToString("000");
    }
}