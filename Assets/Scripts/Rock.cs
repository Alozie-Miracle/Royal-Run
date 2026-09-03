using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisionCooldown = 1f;
    CinemachineImpulseSource cinemachineImpulseSource;
    private float collisionTimer = 1f;


    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCooldown) return; // Ignore collisions if within cooldown
        FireImpuse();
        CollisionFx(collision);
        collisionTimer = 0f; // Reset the timer on collision
    }

    private void FireImpuse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier; // Adjust the intensity based on distance
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFx(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        collisionParticleSystem.transform.position = contact.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
    }
}
