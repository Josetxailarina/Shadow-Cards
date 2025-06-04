using System.Xml;
using TMPro;
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

    [SerializeField] private Sprite windSprite;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite smokeSprite;
    [SerializeField] private Sprite tornadoSprite;
    [SerializeField] private Sprite iceSprite;

    [SerializeField] private TextMeshPro damageTMP;
    [SerializeField] private Animator damageTextAnimator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowDamageText(Vector3 position, int damageAmount)
    {
        damageTMP.text = "-" + damageAmount.ToString();
        damageTMP.transform.parent.transform.position = position;
        damageTextAnimator.SetTrigger("Hit");
    }

    public void PlayEffect(ElementType element, CardState card)
    {
        switch (element)
        {
            case ElementType.Wind:
                card.elementSpriteRenderer.sprite = windSprite;
                windParticles.transform.position = card.transform.position;
                windParticles?.Play();
                windSound?.Play();
                break;
            case ElementType.Fire:
                card.elementSpriteRenderer.sprite = fireSprite;
                fireParticles.transform.position = card.transform.position;
                fireParticles?.Play();
                fireSound?.Play();
                break;
            case ElementType.Water:
                card.elementSpriteRenderer.sprite = waterSprite;
                waterParticles.transform.position = card.transform.position;
                waterParticles?.Play();
                waterSound?.Play();
                break;
            case ElementType.Smoke:
                card.elementSpriteRenderer.sprite = smokeSprite;
                smokeParticles.transform.position = card.transform.position;
                smokeParticles?.Play();
                smokeSound?.Play();
                break;
            case ElementType.Tornado:
                card.elementSpriteRenderer.sprite = tornadoSprite;
                tornadoParticles.transform.position = card.transform.position;
                tornadoParticles?.Play();
                tornadoSound?.Play();
                break;
            case ElementType.Ice:
                card.elementSpriteRenderer.sprite = iceSprite;
                iceParticles.transform.position = card.transform.position;
                iceParticles?.Play();
                iceSound?.Play();
                break;
            
        }
    }
    public void PlayEffect(ElementType element, Vector3 position)
    {
        if (element == ElementType.TornadoAttack)
        {
            tornadoAttackParticles.transform.position = transform.position = new Vector3(position.x, position.y - 1, position.z);
            tornadoAttackParticles?.Play();
            tornadoSound?.Play();
        }
        
    }
}


public enum ElementType
{
    None,
    Wind,
    Fire,
    Water,
    Smoke,
    Tornado,
    Ice,
    TornadoAttack
}