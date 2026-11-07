using System.Collections;
using UnityEngine;

public class HitGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Animation")]
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float scaleMultiplier = 1.2f;

    public void Play(Sprite sprite, Color color, Vector3 scale)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;

        transform.localScale = scale;

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * scaleMultiplier;

        Color startColor = spriteRenderer.color;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / duration);
            t = 1f - Mathf.Pow(1f - t, 3f);

            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, t);

            spriteRenderer.color = c;

            yield return null;
        }

        Destroy(gameObject);
    }
}