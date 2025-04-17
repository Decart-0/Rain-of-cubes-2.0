using TMPro;
using UnityEngine;

public abstract class SpawnerView<T> : MonoBehaviour where T : ISpawnerStatistics
{
    private const string TextSpawnedObjects = "Количество заспавненных объектов: ";
    private const string TextActiveObjects = "Количество активных объектов: ";
    private const string TextCreatedObjects = "Количество созданных объектов: ";

    [SerializeField] private TMP_Text _numberSpawnedObjects;
    [SerializeField] private TMP_Text _numberActiveObjects;
    [SerializeField] private TMP_Text _numberCreatedObjects;
    [SerializeField] private T _spawner;

    private void OnEnable()
    {
        _spawner.StatisticsUpdating += UpdateStatistics;
    }

    private void OnDisable()
    {
        _spawner.StatisticsUpdating -= UpdateStatistics;
    }

    private void UpdateStatistics() 
    {
        _numberSpawnedObjects.text = TextSpawnedObjects + _spawner.NumberSpawnedObjects;
        _numberActiveObjects.text = TextActiveObjects + _spawner.NumberActiveObjects;
        _numberCreatedObjects.text = TextCreatedObjects + _spawner.NumberCreatedObjects;    
    }
}