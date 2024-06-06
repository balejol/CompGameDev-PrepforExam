using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 2f; // Range within which the player can attack
    [SerializeField]
    private int attackDamage = 1;   // Amount of damage dealt per attack
    [SerializeField]
    private float attackCooldown = 1f; // Time between attacks
    [SerializeField]
    private bool applyBleedEffect = false; // Whether to apply bleed effect

    private float lastAttackTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        // Find all enemies within attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Call the TakeDamage method on the enemy
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage, applyBleedEffect);
                    Debug.Log("Player attacked the enemy!");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
