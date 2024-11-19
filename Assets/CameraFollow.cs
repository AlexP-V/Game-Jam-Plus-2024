using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player1;
    [SerializeField] Transform player2; 
    private float cameraZOffset = -10f;

    [SerializeField] private float minOrthographicSize = 5f; 
    [SerializeField] private float maxOrthographicSize = 10f; 
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float distanceThreshold = 5f;

    void Update()
    {
        if (player1 != null && player2 != null)
        {            
            Vector3 midpoint = (player1.position + player2.position) / 2;
                       
            transform.position = new Vector3(midpoint.x, midpoint.y, cameraZOffset);

            float playerDistance = Vector2.Distance(player1.position, player2.position);

            float desiredSize;

            if (playerDistance <= distanceThreshold)
            {               
                desiredSize = minOrthographicSize;
            }
            else
            {
                // If beyond threshold, gradually increase size based on how far past the threshold the players are
                float excessDistance = playerDistance - distanceThreshold;
                float zoomFactor = excessDistance / (maxOrthographicSize - minOrthographicSize); // Normalize excess distance
                desiredSize = Mathf.Clamp(minOrthographicSize + zoomFactor * (maxOrthographicSize - minOrthographicSize), minOrthographicSize, maxOrthographicSize);
            }
                       
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredSize, Time.deltaTime * zoomSpeed);
        }
    }
}

