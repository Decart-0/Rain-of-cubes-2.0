using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _maxForce;
    [SerializeField] private float _minForce;
    [SerializeField] private float _radius;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private void OnEnable()
    {
        _spawnerBomb.Deleted += ExplodeCube;
    }

    private void OnDisable()
    {
        _spawnerBomb.Deleted -= ExplodeCube;
    }

    private void ExplodeCube(Vector3 position)
    {
        List<Rigidbody> cubes = GetExplodableCubes(position);

        foreach (Rigidbody explodableObject in cubes)
        {
            float distance = Vector3.Distance(position, explodableObject.position);
            Vector3 direction = (explodableObject.position - position).normalized;
            float explosionForce = Mathf.Clamp(_maxForce / (distance + 1), _minForce, _maxForce);
            explodableObject.AddForce(direction * explosionForce, ForceMode.Impulse);
        }
    }

    private List<Rigidbody> GetExplodableCubes(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _radius);
        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}
