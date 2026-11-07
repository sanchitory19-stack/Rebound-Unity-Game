using UnityEngine;

public class AimLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void ShowLine(Vector3 start, Vector3 end)
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void HideLine()
    {
        lineRenderer.enabled = false;
    }
}