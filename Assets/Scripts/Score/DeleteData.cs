using UnityEngine;

/// <summary>
/// テストように今までのランキングデータを削除するよう
/// </summary>
public class DeleteData : MonoBehaviour
{
    public void OnDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
