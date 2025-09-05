using UnityEngine;

public class SwordDmg : MonoBehaviour
{
    [SerializeField] private float attackDamage = 40f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}
