
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool IsLaunched { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        if (IsLaunched)
            return;

        Debug.Log("Launch Called");
        IsLaunched = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void DestroyBall()
    {
        if (IsLaunched)
            return;

        FindAnyObjectByType<InputManager>().ClearCurrentBall();

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("wall"))
        {
            Debug.Log("WALL DETECTED!");
            AudioManager.Instance.PlayWallBounce();
        }
    }

    private void OnBecameInvisible()
    {
        if (!IsLaunched)
            return;

        GameManager.Instance.ResetLevel();

        FindAnyObjectByType<InputManager>().ClearCurrentBall();

        Destroy(gameObject);
    }
}
