using UnityEngine;

public class AimDots : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int dotCount = 10;
    [SerializeField] private float spacing = 0.35f;

    [Header("Appearance")]
    [SerializeField] private float dotSize = 0.1f;
    [SerializeField] private float smallestDotMultiplier = 0.3f;
    [SerializeField] private float finalAlpha = 0.15f;

    private Transform[] dots;
    private SpriteRenderer[] renderers;

    private void Awake()
    {
        dots = new Transform[dotCount];
        renderers = new SpriteRenderer[dotCount];

        for (int i = 0; i < dotCount; i++)
        {
            GameObject dot = Instantiate(dotPrefab, transform);
            dot.SetActive(false);

            dots[i] = dot.transform;
            renderers[i] = dot.GetComponent<SpriteRenderer>();
        }
    }

    public void Show(Vector3 start, Vector2 direction)
    {
        for (int i = 0; i < dotCount; i++)
        {
            GameObject dot = dots[i].gameObject;
            dot.SetActive(true);

            dots[i].position =
                start + (Vector3)(direction * spacing * (i + 1));

            // Size
            float scale = Mathf.Lerp(
                dotSize,
                dotSize * smallestDotMultiplier,
                (float)i / (dotCount - 1));

            dots[i].localScale = Vector3.one * scale;

            // Transparency
            Color color = renderers[i].color;
            color.a = Mathf.Lerp(
                1f,
                finalAlpha,
                (float)i / (dotCount - 1));

            renderers[i].color = color;
        }
    }

    public void Hide()
    {
        foreach (Transform dot in dots)
        {
            dot.gameObject.SetActive(false);
        }
    }
}