using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _force;

    public void Explode()
    {
        Collider[] overlappedCollider = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < overlappedCollider.Length; i++)
        {
            Rigidbody rigidbody = overlappedCollider[i].attachedRigidbody;

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_force, transform.position, _radius);
            }
        }
    }
}