using UnityEngine;

public class MoveToTitle : MonoBehaviour
{
    public void MoveToTitleScene()
    {
        AudioManager.Instance.PlaySE("Click");
        SceneLoader.LoadScene("InGame");
    }
}
