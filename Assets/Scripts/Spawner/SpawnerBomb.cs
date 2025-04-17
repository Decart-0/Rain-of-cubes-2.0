using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>, ISpawnerStatistics
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private Vector3 _spawnPosition;

    private void OnEnable()
    {
        _spawnerCube.Deleted += OnCubeDeleted;
    }

    private void OnDisable()
    {
        _spawnerCube.Deleted -= OnCubeDeleted;
    }

    protected override Vector3 GetSpawnPoint()
    {
        return _spawnPosition;
    }

    private void OnCubeDeleted(Vector3 spawnPosition)
    {
        _spawnPosition = spawnPosition;
        GetPrefab();
    }
}