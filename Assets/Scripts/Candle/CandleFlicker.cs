using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    [SerializeField] float speed = 2f;
    [SerializeField] float scaleAmount = 0.05f;

    [Header("Color Settings")]
    [SerializeField] Color colorA = new Color(1f, 0.9f, 0.2f);
    [SerializeField] Color colorB = new Color(1f, 0.4f, 0f);

    Vector3 baseScale;
    float seed;
    SpriteRenderer sr;

    private void Start()
    {
        baseScale = transform.localScale;
        seed = Random.Range(0f, 100f);
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float t = Time.time * speed + seed;

        // スケール揺れ
        float noise = Mathf.PerlinNoise(t, 0f);
        float scale = 1f + (noise - 0.5f) * 2f * scaleAmount;
        transform.localScale = baseScale * scale;

        // 色の揺らぎ
        float colNoise = Mathf.PerlinNoise(t + 200f, 0f);
        sr.color = Color.Lerp(colorA, colorB, colNoise);
    }
}