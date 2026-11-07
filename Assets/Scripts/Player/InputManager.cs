using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private AimDots aimDots;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float minAimDistance = 0.1f;
    [SerializeField] private float spawnCheckRadius = 0.15f;
    [SerializeField] private LayerMask spawnBlockedLayers;

    private Camera mainCamera;
    private GameObject currentBall;
    private bool isAiming;
    private bool hasAimDirection;
    private Vector2 aimDirection;
    private BallController currentBallController;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartAim();
        }

        if (isAiming && Mouse.current.leftButton.isPressed)
        {
            UpdateAim();
        }

        if (isAiming && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ShootBall();
        }
    }

    private void StartAim()
    {
        Debug.Log("Start Aim");

        if (currentBallController != null && currentBallController.IsLaunched)
        {
            return;
        }

        ResetCurrentShot();

        Vector3 worldPos = GetPointerWorldPosition();

        Collider2D hit = Physics2D.OverlapCircle(
            worldPos,
            spawnCheckRadius,
            spawnBlockedLayers);

        if (hit != null)
        {
            Debug.Log("Cannot spawn ball here.");
            return;
        }

        currentBall = Instantiate(ballPrefab, worldPos, Quaternion.identity);
        currentBallController = currentBall.GetComponent<BallController>();

        isAiming = true;
        hasAimDirection = false;
        aimDirection = Vector2.zero;

        aimDots.Hide();
    }

    private void ResetCurrentShot()
    {
        if (currentBall == null)
            return;

        Destroy(currentBall);
        currentBall = null;
        currentBallController = null;

        GameManager.Instance.ResetLevel();
    }

    private void UpdateAim()
    {
        Debug.Log("Dragging");

        if (currentBall == null)
            return;

        Vector3 mousePos = GetPointerWorldPosition();
        Vector3 start = currentBall.transform.position;
        Vector3 direction = start - mousePos;

        if (direction.sqrMagnitude < minAimDistance * minAimDistance)
        {
            hasAimDirection = false;
            aimDirection = Vector2.zero;
            aimDots.Hide();
            return;
        }

        hasAimDirection = true;
        aimDirection = direction.normalized;

        aimDots.Show(start, aimDirection);
    }

    private void ShootBall()
    {
        Debug.Log("Shoot");

        if (currentBall == null)
            return;

        if (!hasAimDirection)
        {
            Debug.Log("Shoot cancelled: aim distance too short");

            Destroy(currentBall);
            currentBall = null;
            currentBallController = null;
            isAiming = false;

            aimDots.Hide();
            return;
        }

        currentBallController.Launch(aimDirection, launchForce);

        isAiming = false;
        hasAimDirection = false;

        aimDots.Hide();
    }

    private Vector3 GetPointerWorldPosition()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        worldPosition.z = 0;
        return worldPosition;
    }

    public void ClearCurrentBall()
    {
        Debug.Log("Clear Current Ball");

        currentBall = null;
        currentBallController = null;
        isAiming = false;
        hasAimDirection = false;
        aimDirection = Vector2.zero;

        aimDots.Hide();
    }
}