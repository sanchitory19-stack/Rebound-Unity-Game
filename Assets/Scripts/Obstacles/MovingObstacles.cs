using UnityEngine;

public class MovingObstacle : Target
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    [Header("Timing")]
    [SerializeField]
    [Range(0f, 1f)]
    private float startOffset = 0f;

    private float pathLength;

    private void Start()
    {
        pathLength = Vector3.Distance(pointA.position, pointB.position);
    }

    private void Update()
    {
        if (pathLength <= 0f)
            return;

        // Apply an offset so different obstacles can start at different points
        float distance = Mathf.PingPong(
            (Time.time * speed) + (startOffset * pathLength),
            pathLength);

        float t = distance / pathLength;

        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}