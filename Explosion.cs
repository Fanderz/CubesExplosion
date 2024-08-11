using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private float _explosionRadius = 500f;

    private void Start()
    {
        this.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, this.transform.position, _explosionRadius);
    }
}
