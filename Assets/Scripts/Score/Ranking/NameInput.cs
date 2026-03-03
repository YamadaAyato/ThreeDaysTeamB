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
        nameInput.text = "";    //入力フィールドを初期化
    }

    /// <summary>
    /// 入力された文字列をチェックし、空の場合は「プレイヤー」というデフォルト名を割り当てた上で、PlayerPrefs に保存します
    /// </summary>
    public void SaveName()
    {
        if (nameInput == null)
        {
            Debug.LogError("NameInputがnullです！", this.gameObject);
            return;
        }
       string playerName = nameInput.text;

        if (string.IsNullOrEmpty(playerName))
            playerName = "プレイヤー";

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }
}