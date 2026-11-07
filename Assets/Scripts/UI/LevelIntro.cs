using System.Collections;
using TMPro;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private RectTransform textTransform;

    [Header("Animation")]
    [SerializeField] private float centerY = 0f;
    [SerializeField] private float endY = 700f;
    [SerializeField] private float slideSpeed = 900f;
    [SerializeField] private float pauseTime = 0.6f;

    public void Play(int levelNumber)
    {
        levelText.text = levelNumber.ToString();

        StopAllCoroutines();
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        // Instantly appear in the center
        textTransform.anchoredPosition = new Vector2(
            textTransform.anchoredPosition.x,
            centerY);

        // Stay for a moment
        yield return new WaitForSeconds(pauseTime);

        // Slide upward off-screen
        float currentSpeed = 0f;
        float acceleration = 3000f;

        while (textTransform.anchoredPosition.y < endY)
        {
            currentSpeed += acceleration * Time.deltaTime;

            textTransform.anchoredPosition +=
                Vector2.up * currentSpeed * Time.deltaTime;

            yield return null;
        }

        textTransform.anchoredPosition = new Vector2(
            textTransform.anchoredPosition.x,
            endY);

        LevelManager.Instance.LevelIntroFinished();
    }
}