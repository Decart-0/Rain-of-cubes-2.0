using System;

public interface ISpawnerStatistics
{
    public event Action StatisticsUpdating;

    public int NumberSpawnedObjects { get; }

    public int NumberActiveObjects { get; }

    public int NumberCreatedObjects { get; }
}