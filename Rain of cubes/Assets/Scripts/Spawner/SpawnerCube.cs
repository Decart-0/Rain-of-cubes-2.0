using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCube : Spawner<Cube>, ISpawnerStatistics
{
    [SerializeField] private int _heightSpawn = 7;
    [SerializeField] private float _repeatRate = 1;
    [SerializeField] private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(SpawnFrequency());
    }

    protected override Vector3 GetSpawnPoint()
    {
        Vector3 colliderSize = Collider.bounds.size;
        float half = 0.5f;
        float maxDistance = Mathf.Min(colliderSize.x, colliderSize.z) * half;
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);
        float randomDistance = Random.Range(0f, maxDistance);
        float x = transform.position.x + Mathf.Cos(randomAngle) * randomDistance;
        float z = transform.position.z + Mathf.Sin(randomAngle) * randomDistance;
        float y = transform.position.y + _heightSpawn;

        return new Vector3(x, y, z);
    }

    private IEnumerator SpawnFrequency()
    {
        while (_isSpawning)
        {
            GetPrefab();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
}