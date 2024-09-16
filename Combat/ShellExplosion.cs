using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    [SerializeField]private LayerMask _TankMask;                        
    [SerializeField]private ParticleSystem _ExplosionParticles;         
    [SerializeField]private AudioSource _ExplosionAudio;                
    
    private float _MaxDamage = 100f;                    
    private float _ExplosionForce = 500f;              
    private float _MaxLifeTime = 2f;                    
    private float _ExplosionRadius = 5f;
    private Collider[] _results;

    private void Start()
    {
        _results = new Collider[5];
        Destroy(gameObject, _MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _ExplosionRadius,_results,_TankMask);
        Debug.Log("Number Of Colliders : "+numColliders);

        for (int i = 0; i < numColliders; i++)
        {

            Rigidbody targetRigidbody = _results[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(_ExplosionForce, transform.position, _ExplosionRadius);

            Health targetHealth = targetRigidbody.GetComponent<Health>();

            if (!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidbody.position);

            targetHealth.TakeDamage(damage);
        }

        _ExplosionParticles.transform.parent = null;

        _ExplosionParticles.Play();

        _ExplosionAudio.Play();

        Destroy(_ExplosionParticles.gameObject, _ExplosionParticles.main.duration);

        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (_ExplosionRadius - explosionDistance) / _ExplosionRadius;

        float damage = relativeDistance * _MaxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
