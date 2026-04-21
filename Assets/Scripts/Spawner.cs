using UnityEngine;

public class Spawner : MonoBehaviour
{

    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;

        [Range(0f, 1f)]
        public float spawnChance;

        public float minHeight;
        public float maxHeight;
    }

    public SpawnableObject[] obstacles;

    public float minSpawnerRate = 1f;
    public float maxSpawnerRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnerRate, maxSpawnerRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach(var obstacle in obstacles)
        {
            if (spawnChance < obstacle.spawnChance)
            {
                GameObject obstacleInstance = Instantiate(obstacle.prefab);

                float currentZ = transform.position.z;
                float obstacleY = Random.Range(obstacle.minHeight, obstacle.maxHeight);
                obstacleInstance.transform.position = new Vector3(12f, obstacleY, currentZ);
                break;
            }

            spawnChance -= obstacle.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnerRate, maxSpawnerRate));
    }
}
