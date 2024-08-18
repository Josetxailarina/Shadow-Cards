using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Static AudioSource references
    public static AudioSource windSound;
    public static AudioSource fireSound;
    public static AudioSource waterSound;
    public static AudioSource smokeSound;
    public static AudioSource tornadoSound;
    public static AudioSource iceSound;

    // Public references for AudioSource (to assign in Inspector)
    public AudioSource windSoundRef;
    public AudioSource fireSoundRef;
    public AudioSource waterSoundRef;
    public AudioSource smokeSoundRef;
    public AudioSource tornadoSoundRef;
    public AudioSource iceSoundRef;

    // Static ParticleSystem references
    public static ParticleSystem windParticles;
    public static ParticleSystem fireParticles;
    public static ParticleSystem waterParticles;
    public static ParticleSystem smokeParticles;
    public static ParticleSystem tornadoParticles;
    public static ParticleSystem iceParticles;

    // Public references for ParticleSystem (to assign in Inspector)
    public ParticleSystem windParticlesRef;
    public ParticleSystem fireParticlesRef;
    public ParticleSystem waterParticlesRef;
    public ParticleSystem smokeParticlesRef;
    public ParticleSystem tornadoParticlesRef;
    public ParticleSystem iceParticlesRef;

    private void Start()
    {
        // Assign AudioSource references
        windSound = windSoundRef;
        fireSound = fireSoundRef;
        waterSound = waterSoundRef;
        smokeSound = smokeSoundRef;
        tornadoSound = tornadoSoundRef;
        iceSound = iceSoundRef;

        // Assign ParticleSystem references
        windParticles = windParticlesRef;
        fireParticles = fireParticlesRef;
        waterParticles = waterParticlesRef;
        smokeParticles = smokeParticlesRef;
        tornadoParticles = tornadoParticlesRef;
        iceParticles = iceParticlesRef;
    }

    // Functions to play sound and particle effects at a specific position
    public static void PlayWindEffect(Vector3 position)
    {
        if (windParticles != null)
        {
            windParticles.transform.position = position;
            windParticles.Play();
        }
        windSound?.Play();
    }

    public static void PlayFireEffect(Vector3 position)
    {
        if (fireParticles != null)
        {
            fireParticles.transform.position = position;
            fireParticles.Play();
        }
        fireSound?.Play();
    }

    public static void PlayWaterEffect(Vector3 position)
    {
        if (waterParticles != null)
        {
            waterParticles.transform.position = position;
            waterParticles.Play();
        }
        waterSound?.Play();
    }

    public static void PlaySmokeEffect(Vector3 position)
    {
        if (smokeParticles != null)
        {
            smokeParticles.transform.position = position;
            smokeParticles.Play();
        }
        smokeSound?.Play();
    }

    public static void PlayTornadoEffect(Vector3 position)
    {
        if (tornadoParticles != null)
        {
            tornadoParticles.transform.position = position;
            tornadoParticles.Play();
        }
        tornadoSound?.Play();
    }

    public static void PlayIceEffect(Vector3 position)
    {
        if (iceParticles != null)
        {
            iceParticles.transform.position = position;
            iceParticles.Play();
        }
        iceSound?.Play();
    }
}
