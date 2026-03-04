using UnityEngine;

public class MoveToTitle : MonoBehaviour
{
    public void MoveToTitleScene()
    {
        SceneLoader.LoadScene("InGame");
    }
}
