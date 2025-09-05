using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;
   

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float showHealthDistance = 10f;

    private Transform player;
    private float currentHealth;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();

        float multiplier = GameManager.Instance.difficulty;
        maxHealth *= multiplier;
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        player = GameObject.FindWithTag("Player").transform;
        if (healthSlider != null)
            healthSlider.gameObject.SetActive(false);

       
    }
    private void Update()
    {
        if (player == null) return;
        if (healthSlider != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            healthSlider.gameObject.SetActive(distance <= showHealthDistance);
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die(); 
        }
        else 
        {
            animator.SetTrigger("TakeDmg");
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        WaveManager waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        if (waveManager != null)
            waveManager.EnemyDied();
        Destroy(gameObject,2f);
    }

   
}
