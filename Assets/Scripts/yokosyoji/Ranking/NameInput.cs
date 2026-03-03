using UnityEngine;
using TMPro;
using System.Xml.Serialization;

/// <summary>
/// 名前の入力を受け取る　名前の登録ができるとこ
/// </summary>

public class NameInput : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;


    private void Start()
    {
        nameInput.text = "";
    }
    public void SaveName()
    {
        if (nameInput == null)
        {
            Debug.LogError("NameInputがnullです！", this.gameObject);
            return;
        }
        name = nameInput.text;

        if (string.IsNullOrEmpty(name))
            name = "プレイヤー";

        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();
    }
}