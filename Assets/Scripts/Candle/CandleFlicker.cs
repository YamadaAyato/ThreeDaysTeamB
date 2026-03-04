using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _scaleAmount = 0.05f;

    [Header("Color Settings")]
    [SerializeField] private Color _brightColor = new Color(1f, 0.9f, 0.2f);
    [SerializeField] private Color _dimColor = new Color(1f, 0.4f, 0f);

    private Vector3 _baseScale;
    private float _seed;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _baseScale = transform.localScale;
        _seed = Random.Range(0f, 100f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float noiseTime = Time.time * _speed + _seed;

        // スケール揺れ
        float noise = Mathf.PerlinNoise(noiseTime, 0f);
        float scale = 1f + (noise - 0.5f) * 2f * _scaleAmount;
        transform.localScale = _baseScale * scale;

        // 色の揺らぎ
        float colorNoise = Mathf.PerlinNoise(noiseTime + 200f, 0f);
        _spriteRenderer.color = Color.Lerp(_brightColor, _dimColor, colorNoise);
    }
}