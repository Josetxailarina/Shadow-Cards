using TMPro;
using UnityEngine;

public class CountersManager : MonoBehaviour
{
    // Singleton instance for CountersManager
    public static CountersManager instance;

    // Current health and mana values
    private int currentPlayerHealth = 30;
    private int currentBossHealth = 30;
    private int currentPlayerMana = 1;

    // Animator references
    private Animator playerHealthAnimator;
    private Animator bossHealthAnimator;
    private Animator playerManaAnimator;

    // UI references
    [SerializeField] private TextMeshPro playerHealthTMP;
    [SerializeField] private TextMeshPro bossHealthTMP;
    [SerializeField] private TextMeshPro playerManaTMP;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        playerHealthAnimator = playerHealthTMP.GetComponent<Animator>();
        bossHealthAnimator = bossHealthTMP.GetComponent<Animator>();
        playerManaAnimator = playerManaTMP.GetComponent<Animator>();
    }

    void Start()
    {
        SetupInitialStats();
    }

    
    public void ReducePlayerHealth(int amount)
    {
        currentPlayerHealth -= amount;
        playerHealthTMP.text = currentPlayerHealth.ToString();
        playerHealthAnimator.SetTrigger("Hit");
        if (currentPlayerHealth <= 0) 
        { 
            losePanel.SetActive(true);
            GameManager.gameState = GameState.Menu;

        }
    }

    public void ReduceBossHealth(int amount)
    {
        currentBossHealth -= amount;
        bossHealthTMP.text = currentBossHealth.ToString();
        bossHealthAnimator.SetTrigger("Hit");
        if (currentBossHealth <= 0)
        {
            winPanel.SetActive(true);
            GameManager.gameState = GameState.Menu;
        }
    }


    public void ReducePlayerMana(int amount)
    {
        currentPlayerMana -= amount;
        playerManaTMP.text = currentPlayerMana.ToString();

    }


    private void SetupInitialStats()
    {
        currentPlayerHealth = 30;
        currentBossHealth = 30;
        currentPlayerMana = 1;
        UpdateStats();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void UpdateStats()
    {
        playerManaTMP.text = currentPlayerMana.ToString();
        playerHealthTMP.text = currentPlayerHealth.ToString();
        bossHealthTMP.text = currentBossHealth.ToString();
    }

    public bool CanPayManaCost(int cost)
    {
        return currentPlayerMana >= cost;
    }
    public void SetCurrentMana(int mana)
    {
        if (mana > 10)
        {
            mana = 10;
        }
        currentPlayerMana = mana;
        playerManaTMP.text = currentPlayerMana.ToString();
    }
   
    public void ShowManaInsufficient()
    {
        playerManaAnimator.SetTrigger("Hit");
        SoundManager.instance.deniedSound.Play();
    }
}
