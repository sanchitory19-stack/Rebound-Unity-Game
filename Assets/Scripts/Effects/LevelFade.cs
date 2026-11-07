using System.Collections;
using UnityEngine;

public class LevelFade : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 0.5f;

    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public IEnumerator FadeOut()
    {
        Color[] originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / fadeDuration);

            // Ease out for a smoother fade
            t = 1f - Mathf.Pow(1f - t, 3f);

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Color c = originalColors[i];
                c.a = Mathf.Lerp(1f, 0f, t);
                spriteRenderers[i].color = c;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }
}