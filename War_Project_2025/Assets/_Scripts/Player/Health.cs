using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    Animator animator;

    [SerializeField] private GameObject gameOverPanel;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void Update()
    {
        
    }

    // Call this when the player takes damage
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    // Call this when healing
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    private void Die()
    {
        InputManager.Instance.DisableAllInputs();
        // Save stats when player dies
        StatsManager.SaveStats();
        gameOverPanel.SetActive(true);
        animator.SetTrigger("Die");
        Destroy(gameObject,2f);
    }


}
