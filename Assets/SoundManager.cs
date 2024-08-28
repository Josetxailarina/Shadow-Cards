using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    // Static AudioSource references
    public static AudioSource windSound;
    public static AudioSource fireSound;
    public static AudioSource waterSound;
    public static AudioSource smokeSound;
    public static AudioSource tornadoSound;
    public static AudioSource iceSound;
    public static AudioSource iceBreakSound;
    public static AudioSource useCardSound;
    public static AudioSource selectCardSound;
    public static AudioSource takeCardSound;

    // Public references for AudioSource (to assign in Inspector)
    public AudioSource windSoundRef;
    public AudioSource fireSoundRef;
    public AudioSource waterSoundRef;
    public AudioSource smokeSoundRef;
    public AudioSource tornadoSoundRef;
    public AudioSource iceSoundRef;
    public AudioSource iceBreakSoundRef;
    public  AudioSource useCardSoundRef;
    public  AudioSource selectCardSoundRef;
    public  AudioSource takeCardSoundRef;

    // Static ParticleSystem references
    public static ParticleSystem windParticles;
    public static ParticleSystem fireParticles;
    public static ParticleSystem waterParticles;
    public static ParticleSystem smokeParticles;
    public static ParticleSystem tornadoParticles;
    public static ParticleSystem iceParticles;
    public static ParticleSystem tornadoAttackParticles;


    // Public references for ParticleSystem (to assign in Inspector)
    public ParticleSystem windParticlesRef;
    public ParticleSystem fireParticlesRef;
    public ParticleSystem waterParticlesRef;
    public ParticleSystem smokeParticlesRef;
    public ParticleSystem tornadoParticlesRef;
    public ParticleSystem iceParticlesRef;
    public ParticleSystem tornadoAttackParticlesRef;

    private void Start()
    {
        // Assign AudioSource references
        windSound = windSoundRef;
        fireSound = fireSoundRef;
        waterSound = waterSoundRef;
        smokeSound = smokeSoundRef;
        tornadoSound = tornadoSoundRef;
        iceSound = iceSoundRef;
        iceBreakSound = iceBreakSoundRef;
        useCardSound = useCardSoundRef;
        selectCardSound = selectCardSoundRef;
        takeCardSound = takeCardSoundRef;
        // Assign ParticleSystem references
        windParticles = windParticlesRef;
        fireParticles = fireParticlesRef;
        waterParticles = waterParticlesRef;
        smokeParticles = smokeParticlesRef;
        tornadoParticles = tornadoParticlesRef;
        iceParticles = iceParticlesRef;
        tornadoAttackParticles = tornadoAttackParticlesRef;
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
    public static void PlayTornadoAttackEffect(Vector3 position)
    {
        if (tornadoAttackParticles != null)
        {
            tornadoAttackParticles.transform.position = new Vector3(position.x,position.y - 1,position.z);
            tornadoAttackParticles.Play();
        }
        tornadoSound?.Play();
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
