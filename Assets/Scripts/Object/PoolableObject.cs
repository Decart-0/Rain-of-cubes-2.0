using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class PoolableObject<T> : MonoBehaviour where T : MonoBehaviour, ISpawner<T>
{
    protected Renderer Renderer;

    [SerializeField] private Color _defaultColor;

    public event Action<T> LifeTimeExpired;

    public int LifeTime { get; protected set; }

    public virtual void Init()
    {
        int minTime = 5;
        int maxTime = 5;

        LifeTime = UnityEngine.Random.Range(minTime, maxTime + 1);
        Renderer.material.color = _defaultColor;
    }

    protected void OnAwake()
    {
        Renderer = GetComponent<Renderer>();
    }

    protected void NotifyLifeTimeExpired()
    {
        LifeTimeExpired?.Invoke(this as T);
    }

    protected virtual IEnumerator DecreaseLifeTime()
    {
        yield return new WaitForSeconds(LifeTime);
        NotifyLifeTimeExpired();
    }
}