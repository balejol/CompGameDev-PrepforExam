using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstacles; // List of obstacle prefabs

    [Min(0)]
    [SerializeField]
    private int count = 2; // Number of obstacles to generate

    [SerializeField]
    private Vector3 size = new Vector3(1f, 0f, 1f); // Size of the volume to spawn obstacles in

    [SerializeField]
    private Vector3 minSize = new Vector3(0.5f, 0.5f, 0.5f); // Minimum size of the obstacle

    [SerializeField]
    private Vector3 maxSize = new Vector3(2f, 2f, 2f); // Maximum size of the obstacle

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size); // Draw the spawn volume in the Scene view
    }

    private void Start()
    {
        CreateObstacles();
    }

    private void CreateObstacles()
    {
        for (int i = 0; i < count; i++)
        {
            foreach (var obstacle in obstacles)
            {
                CreateObstacle(obstacle);
            }
        }
    }

    private void CreateObstacle(GameObject obstacle)
    {
        // Instantiate the obstacle at a random position within the defined volume
        GameObject newObstacle = Instantiate(
            obstacle,
            GetRandomPosition(),
            obstacle.transform.rotation,
            gameObject.transform
        );

        // Apply a random scale to the obstacle
        newObstacle.transform.localScale = GetRandomSize();
    }

    private Vector3 GetRandomPosition()
    {
        // Get a random position within the volume
        var volumePosition = new Vector3(
            Random.Range(0, size.x),
            Random.Range(0, size.y),
            Random.Range(0, size.z)
        );

        return transform.position + volumePosition - size / 2;
    }

    private Vector3 GetRandomSize()
    {
        // Generate a random size for the obstacle within the min and max size range
        float randomX = Random.Range(minSize.x, maxSize.x);
        float randomY = Random.Range(minSize.y, maxSize.y);
        float randomZ = Random.Range(minSize.z, maxSize.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
