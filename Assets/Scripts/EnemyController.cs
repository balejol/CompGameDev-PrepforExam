using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform player;           // Reference to the player's transform
    [SerializeField]
    private float moveSpeed = 6f;       // Movement speed of the enemy
    [SerializeField]
    private float attackRange = 2f;     // Range within which the enemy can attack
    [SerializeField]
    private float attackCooldown = 1f; // Time between attacks
    [SerializeField]
    private bool applyBleedEffect = false; // Whether to apply bleed effect

    private Rigidbody rb;
    private bool isAttacking = false;
    private float lastAttackTime;

    private Player playerScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Ensure the player reference is set
        if (player == null) return;

        // Calculate the direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards the player if not in attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // If the player script is available, call TakeDamage
        if (playerScript != null)
        {
            playerScript.TakeDamage(1, applyBleedEffect);  // Assuming fixed damage of 1 for the attack
            Debug.Log("Enemy attacks the player!");
        }

        // Wait for the cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
