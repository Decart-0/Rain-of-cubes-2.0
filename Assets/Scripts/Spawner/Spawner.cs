using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawner<T>
{
    protected Collider Collider;
    
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolMaxSize = 7;
    [SerializeField] private int _poolCapacity = 7;

    private ObjectPool<T> _pool;

    public event Action StatisticsUpdating;
    public event Action<Vector3> Deleted;

    public int NumberSpawnedObjects { get; private set; } = 0;

    public int NumberActiveObjects { get; private set; }  = 0;

    public int NumberCreatedObjects { get; private set; } = 0;

    private void Awake()
    {
        Collider = GetComponent<Collider>();

        _pool = new ObjectPool<T>
        (
            createFunc: () => CreateNewObject(),
            actionOnGet: (T) => ExecuteOnGet(T),
            actionOnRelease: (T) => T.gameObject.SetActive(false),
            actionOnDestroy: (T) => Destroy(T),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    protected virtual Vector3 GetSpawnPoint()
    {
        return transform.position;
    }

    protected void GetPrefab()
    {
        _pool.Get();
    }

    private void ReturnObjectPool(T prefab)
    {
        prefab.LifeTimeExpired -= ReturnObjectPool;
        NumberActiveObjects--;
        StatisticsUpdating?.Invoke();

        Vector3 cubePosition = prefab.transform.position;
        _pool.Release(prefab);
        Deleted?.Invoke(cubePosition);
    }

    private void ExecuteOnGet(T prefab)
    {
        NumberActiveObjects++;
        NumberSpawnedObjects++;
        StatisticsUpdating?.Invoke();

        prefab.LifeTimeExpired += ReturnObjectPool;
        prefab.transform.position = GetSpawnPoint();
        prefab.gameObject.SetActive(true);

        SetupPrefab(prefab);
        SetupRigidbody(prefab);
    }

    private void SetupPrefab(T prefab)
    {
        prefab.Init();
    }

    private T CreateNewObject()
    {
        NumberCreatedObjects++;
        StatisticsUpdating?.Invoke();

        return Instantiate(_prefab);
    }

    private void SetupRigidbody(T prefab)
    {
        Rigidbody rigidbody = prefab.GetComponent<Rigidbody>();

        if (rigidbody == null)
            rigidbody = prefab.gameObject.AddComponent<Rigidbody>();

        rigidbody.velocity = Vector3.zero;
    }
}