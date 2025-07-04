using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class AIObstacle
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void InitObstacles()
    {
        Rigidbody[] allBodies = GameObject.FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in allBodies)
        {
            if (rb.gameObject.GetComponent<NavMeshAgent>()) continue;
            GameObject obj = rb.gameObject;

            // Skip if no collider (NavMeshObstacle requires one)
            Collider col = obj.GetComponent<Collider>();
            if (col == null) continue;

            // Add NavMeshObstacle if it doesn't exist
            NavMeshObstacle obstacle = obj.GetComponent<NavMeshObstacle>();
            if (obstacle == null) obstacle = obj.AddComponent<NavMeshObstacle>();

            // Configure obstacle settings
            obstacle.carving = true;
            obstacle.carveOnlyStationary = true;
            obstacle.shape = NavMeshObstacleShape.Box;

            // Match obstacle size to collider
            obstacle.size = col.bounds.size;
            obstacle.center = col.bounds.center - obj.transform.position;

            Debug.Log($"[AIObstacleInitializer] Obstacle set on {obj.name}");
        }
    }
}
