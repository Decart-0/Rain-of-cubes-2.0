using UnityEngine;

public class Cube : PoolableObject<Cube>, ISpawner<Cube>
{
    [SerializeField] private Color _color = Color.black;

    public bool IsThereCollision { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IsThereCollision = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (IsThereCollision == false)
        {
            if (collider.GetComponent<Platform>() != null)
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