using UnityEngine;

public class RotatingObstacle : Target
{
    [SerializeField] private float rotationSpeed = 90f;

    private float initialRotation;

    private void Start()
    {
        // Remember the rotation set in the editor
        initialRotation = transform.eulerAngles.z;
    }

    private void Update()
    {
        float angle = initialRotation + Time.time * rotationSpeed;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}