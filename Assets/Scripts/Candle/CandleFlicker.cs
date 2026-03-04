using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CandleFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    [SerializeField] float speed = 2f;
    [SerializeField] float scaleAmount = 0.05f;

    [Header("Color Settings")]
    [SerializeField] Color colorA = new Color(1f, 0.9f, 0.2f);
    [SerializeField] Color colorB = new Color(1f, 0.4f, 0f);

    // 定数でノイズオフセットを明示
    const float COLOR_OFFSET = 200f;

    Vector3 baseScale;
    float seed;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
        seed = Random.Range(0f, 100f);
    }

    void Update()
    {
        float t = Time.time * speed + seed;
        UpdateScale(t);
        UpdateColor(t);
    }

    void UpdateScale(float t)
    {
        float noise = Mathf.PerlinNoise(t, 0f);
        float scale = 1f + (noise - 0.5f) * 2f * scaleAmount;
        transform.localScale = baseScale * scale;
    }

    void UpdateColor(float t)
    {
        float noise = Mathf.PerlinNoise(t + COLOR_OFFSET, 0f);
        sr.color = Color.Lerp(colorA, colorB, noise);
    }
}