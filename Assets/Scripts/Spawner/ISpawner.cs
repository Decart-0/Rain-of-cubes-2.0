using System;

public interface ISpawner<T>
{
    public void Init();

    public event Action<T> LifeTimeExpired;
}