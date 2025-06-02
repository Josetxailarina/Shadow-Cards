using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }

    [SerializeField] private ParticleSystem windParticles;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private ParticleSystem waterParticles;
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private ParticleSystem tornadoParticles;
    [SerializeField] private ParticleSystem iceParticles;
    [SerializeField] private ParticleSystem tornadoAttackParticles;

    [SerializeField] private AudioSource windSound;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private AudioSource smokeSound;
    [SerializeField] private AudioSource tornadoSound;
    [SerializeField] private AudioSource iceSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    

    public void PlayEffect(ParticleType type, Vector3 position)
    {
        switch (type)
        {
            case ParticleType.Wind:
                windParticles.transform.position = position;
                windParticles?.Play();
                windSound?.Play();
                break;
            case ParticleType.Fire:
                fireParticles.transform.position = position;
                fireParticles?.Play();
                fireSound?.Play();
                break;
            case ParticleType.Water:
                waterParticles.transform.position = position;
                waterParticles?.Play();
                waterSound?.Play();
                break;
            case ParticleType.Smoke:
                smokeParticles.transform.position = position;
                smokeParticles?.Play();
                smokeSound?.Play();
                break;
            case ParticleType.Tornado:
                tornadoParticles.transform.position = position;
                tornadoParticles?.Play();
                tornadoSound?.Play();
                break;
            case ParticleType.Ice:
                iceParticles.transform.position = position;
                iceParticles?.Play();
                iceSound?.Play();
                break;
            case ParticleType.TornadoAttack:
                tornadoAttackParticles.transform.position = transform.position = new Vector3(position.x, position.y - 1, position.z);
                tornadoAttackParticles?.Play();
                tornadoSound?.Play();
                break;
        }
    }
}


public enum ParticleType
{
    Wind,
    Fire,
    Water,
    Smoke,
    Tornado,
    Ice,
    TornadoAttack
}