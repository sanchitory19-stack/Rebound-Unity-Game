using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private LevelIntro levelIntro;

    private GameObject currentLevel;
    private int currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        levelIntro.Play(1);
    }

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;

        levelIntro.Play(currentLevelIndex + 1);
    }

    public void LevelIntroFinished()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        currentLevel = Instantiate(levelPrefabs[currentLevelIndex]);

        GameManager.Instance.InitializeLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelPrefabs.Length)
        {
            Debug.Log("All levels completed!");
            return;
        }

        LoadLevel(currentLevelIndex);
    }

    public void RestartLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        currentLevel = Instantiate(levelPrefabs[currentLevelIndex]);

        GameManager.Instance.InitializeLevel(currentLevel);
    }
    public IEnumerator FadeCurrentLevel()
    {
        if (currentLevel == null)
            yield break;

        LevelFade fade = currentLevel.GetComponent<LevelFade>();

        if (fade != null)
        {
            yield return StartCoroutine(fade.FadeOut());
        }
        else
        {
            currentLevel.SetActive(false);
        }
    }
}