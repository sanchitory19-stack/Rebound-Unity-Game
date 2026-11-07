using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected bool isHit = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D targetCollider;

    [Header("Hit Animation")]
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private HitGhost hitGhostPrefab;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetCollider = GetComponent<Collider2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit)
            return;

        BallController ball = collision.gameObject.GetComponent<BallController>();

        if (ball == null)
            return;

        // If the ball hasn't been launched yet,
        // the obstacle destroys the ball instead.
        if (!ball.IsLaunched)
        {
            ball.DestroyBall();
            return;
        }

        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        isHit = true;

        targetCollider.enabled = false;

        // Save the current visuals
        Sprite currentSprite = spriteRenderer.sprite;
        Color currentColor = spriteRenderer.color;
        Vector3 currentScale = transform.localScale;
        Quaternion currentRotation = transform.rotation;

        // Spawn ghost
        if (hitGhostPrefab != null)
        {
            HitGhost ghost = Instantiate(
                hitGhostPrefab,
                transform.position,
                currentRotation);

            ghost.Play(currentSprite, currentColor, currentScale);
        }

        // Now hide the real target
        spriteRenderer.enabled = false;
        AudioManager.Instance.PlayTargetHit();
        GameManager.Instance.TargetHit();

        gameObject.SetActive(false);

        yield break;
    }
}
