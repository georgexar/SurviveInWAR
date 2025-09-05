using UnityEngine;

public class EnemySwordDmg : MonoBehaviour
{
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyAI.GetDamage());
            }
        }
    }
}
