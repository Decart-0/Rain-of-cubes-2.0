using UnityEngine;

public class Cube : PoolableObject<Cube>, ISpawner<Cube>
{
    [SerializeField] private Color _color = Color.black;

    public bool IsThereCollision { get; private set; }

    private void Awake()
    {
        OnAwake();
        IsThereCollision = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (IsThereCollision == false)
        {
            if (TryGetComponent(out Platform platform))
            {            
                StartCoroutine(DecreaseLifeTime());
                ChangeColor();
                IsThereCollision = true;
            }
        }
    }

    public override void Init() 
    {
        base.Init();       
        IsThereCollision = false;
    }

    private void ChangeColor()
    {
        Renderer.material.color = _color;
    }
}