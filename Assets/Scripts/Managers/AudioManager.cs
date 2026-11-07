using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Game Sounds")]
    [SerializeField] private AudioClip launchClip;
    [SerializeField] private AudioClip targetHitClip;
    [SerializeField] private AudioClip wallBounceClip;
    [SerializeField] private AudioClip levelCompleteClip;
    [SerializeField] private AudioClip levelIntroClip;

    [Header("Pitch Variation")]
    [SerializeField] private float targetHitPitchVariation = 0.04f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayLaunch()
    {
        Play(launchClip);
    }

    public void PlayTargetHit()
    {
        PlayWithPitchVariation(targetHitClip, targetHitPitchVariation);
    }

    public void PlayWallBounce()
    {
        Play(wallBounceClip);
    }

    public void PlayLevelComplete()
    {
        Play(levelCompleteClip);
    }

    public void PlayLevelIntro()
    {
        Play(levelIntroClip);
    }

    private void Play(AudioClip clip)
    {
        if (clip == null)
            return;

        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(clip);
    }

    private void PlayWithPitchVariation(AudioClip clip, float variation)
    {
        if (clip == null)
            return;

        sfxSource.pitch = Random.Range(1f - variation, 1f + variation);
        sfxSource.PlayOneShot(clip);
        sfxSource.pitch = 1f;
    }
}