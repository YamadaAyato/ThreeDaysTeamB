using UnityEngine;

/// <summary>
/// テストように今までのランキングデータを削除するよう
/// </summary>
public class DeleteDeata : MonoBehaviour
{
  public void OnDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
