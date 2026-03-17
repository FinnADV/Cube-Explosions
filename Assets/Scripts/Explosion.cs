using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 700f;
    [SerializeField] private float _upwardsModifier = 3f;

    public void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hitCollider in overlappedColliders)
        {
            Rigidbody attachedRigidbody = hitCollider.attachedRigidbody;

            if (attachedRigidbody != null && attachedRigidbody.isKinematic == false)
            {
                attachedRigidbody.AddExplosionForce(
                    _explosionForce,
                    transform.position,
                    _explosionRadius,
                    _upwardsModifier,
                    ForceMode.Impulse
                );
            }
        }
    }
}