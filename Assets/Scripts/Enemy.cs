using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BleedStatus))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy state")]
    [SerializeField, Min(0)]
    private int livesEnemy = 2;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI livesEnemyText;

    private BleedStatus bleedStatus;

    private void Awake()
    {
        bleedStatus = GetComponent<BleedStatus>();
    }

    private void Start()
    {
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        if (livesEnemyText != null)
        {
            livesEnemyText.text = $"Enemy lives: {livesEnemy}";
        }
    }

    public void TakeDamage(int damage, bool applyBleed = false)
    {
        livesEnemy -= damage;
        UpdateLivesText();
        Debug.Log($"Enemy took {damage} damage. Remaining lives: {livesEnemy}");

        if (applyBleed)
        {
            Debug.Log("Enemy started bleeding!");
            bleedStatus.StartBleeding();
        }

        if (livesEnemy <= 0)
        {
            Die();
        }
    }

    public void TakeBleedDamage(int damage)
    {
        livesEnemy -= damage;
        UpdateLivesText();
        Debug.Log($"Enemy took {damage} bleed damage. Remaining lives: {livesEnemy}");

        if (livesEnemy <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died!");
        Destroy(gameObject);
    }
}
