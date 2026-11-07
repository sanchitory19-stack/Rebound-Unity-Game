
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private RectTransform textTransform;

    [Header("Animation")]
    [SerializeField] private float slideSpeed = 1200f;
    [SerializeField] private float pauseTime = 0.5f;

    private IEnumerator PlayAnimation()
    {
        // Get the actual Canvas height
        RectTransform canvasRect = textTransform.GetComponentInParent<Canvas>()
            .GetComponent<RectTransform>();

        float screenHeight = canvasRect.rect.height;

        // Start at the center
        float centerY = 0f;

        // Move beyond the top edge
        float endY = screenHeight / 2f + textTransform.rect.height + 100f;

        textTransform.anchoredPosition = new Vector2(0f, centerY);

        yield return new WaitForSeconds(pauseTime);

        // Slide upward
        while (textTransform.anchoredPosition.y < endY)
        {
            textTransform.anchoredPosition +=
                Vector2.up * slideSpeed * Time.deltaTime;

            yield return null;
        }

        textTransform.anchoredPosition = new Vector2(0f, endY);

        LevelManager.Instance.LevelIntroFinished();
    }

    public void Play(int levelNumber)
    {
        levelText.text = levelNumber.ToString();

        StopAllCoroutines();
        StartCoroutine(PlayAnimation());
    }
}
