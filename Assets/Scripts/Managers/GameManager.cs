
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int targetsRemaining;
    private bool levelCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void InitializeLevel(GameObject levelRoot)
    {
        levelCompleted = false;

        Target[] targets = levelRoot.GetComponentsInChildren<Target>(false);

        targetsRemaining = targets.Length;

        Debug.Log("Found " + targets.Length + " targets:");

        foreach (Target target in targets)
        {
            Debug.Log(target.name + " | Level: " + levelRoot.name);
        }
    }

    public void TargetHit()
    {
        targetsRemaining--;

        Debug.Log("Targets Remaining: " + targetsRemaining);

        if (targetsRemaining <= 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        if (levelCompleted)
            return;

        StartCoroutine(LevelCompleteRoutine());
    }

    private IEnumerator LevelCompleteRoutine()
    {
        levelCompleted = true;

        Debug.Log("LEVEL COMPLETE!");

        AudioManager.Instance.PlayLevelComplete();

        // Let the final target animation finish
        yield return new WaitForSeconds(0.3f);

        // Hide the level
        yield return StartCoroutine(LevelManager.Instance.FadeCurrentLevel());

        // Small gap before the next level intro
        yield return new WaitForSeconds(0.1f);

        LevelManager.Instance.LoadNextLevel();
    }

    public void ResetLevel()
    {
        if (levelCompleted)
            return;

        Debug.Log("Restarting Level");

        LevelManager.Instance.RestartLevel();
    }
}
