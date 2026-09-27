using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    float spawnRatePerMinute = 30;
    float spawnRateIncrement = 1;
    public float xBorderLimit = 8f;
    public float yBorderLimit = 5f;

    private float spawnNext = 0;

    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;

            var rand = Random.Range(-xBorderLimit, xBorderLimit);
            var spawnPosition = new Vector2(rand, yBorderLimit);

            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
