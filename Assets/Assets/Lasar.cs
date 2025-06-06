using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int maxBounces = 5;
    [SerializeField] private float maxDistance = 300f;
    [SerializeField] private LayerMask reflectiveLayers = -1; // What layers can reflect the laser
    [SerializeField] private bool onlyReflectMirrors = false; // Only reflect off objects tagged "Mirror"

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Set the position count to maxBounces + 1 (start point + bounce points)
        lineRenderer.positionCount = maxBounces + 1;

        // Initialize all positions to prevent errors
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, transform.position);
        }
    }

    void Update()
    {
        CastLaser();
    }

    void CastLaser()
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentDirection = transform.forward; // Changed from -transform.forward

        // Ensure we have enough positions allocated
        if (lineRenderer.positionCount < maxBounces + 1)
        {
            lineRenderer.positionCount = maxBounces + 1;
        }

        // Set the starting position
        lineRenderer.SetPosition(0, currentPosition);

        int actualPositions = 1; // Start with 1 (the starting position)

        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(currentPosition, currentDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, reflectiveLayers))
            {
                // Set the position where the laser hit
                lineRenderer.SetPosition(actualPositions, hit.point);
                actualPositions++;

                // Check if we should stop bouncing
                if (onlyReflectMirrors && !hit.collider.CompareTag("Mirror"))
                {
                    break;
                }

                // Calculate the reflection direction
                currentPosition = hit.point;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);

                // Move slightly away from the surface to prevent self-intersection
                currentPosition += hit.normal * 0.01f;
            }
            else
            {
                // No hit - extend the laser to max distance in current direction
                Vector3 endPoint = currentPosition + currentDirection * maxDistance;
                lineRenderer.SetPosition(actualPositions, endPoint);
                actualPositions++;
                break;
            }
        }

        // Set the actual position count to only show the used points
        lineRenderer.positionCount = actualPositions;
    }
}