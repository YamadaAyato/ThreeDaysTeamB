using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    public int EnemuCounter=0;
   public void UpdetaEnemyCounterUI()
    {
        EnemuCounter++;
        m_TextMeshPro.text=EnemuCounter.ToString();
    }

}
