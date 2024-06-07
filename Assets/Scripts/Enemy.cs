using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BleedStatus))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy state")]
    [SerializeField, Min(0)]
    private int livesEnemy = 10;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI livesEnemyText;

    private BleedStatus bleedStatus;
    private Renderer texture;
    private Color color;
    private int maxLivesEnemy;

    private void Awake()
    {
        bleedStatus = GetComponent<BleedStatus>();
        texture = GetComponent<Renderer>();
        EnsureMaterialSupportsTransparency();
    }

    private void Start()
    {
        maxLivesEnemy = livesEnemy; // Initialize maxLivesEnemy to the starting health
        UpdateLivesText();
        if (texture != null)
        {
            color = texture.material.color; // Initialize color in Start
        }
    }

    private void EnsureMaterialSupportsTransparency()
    {
        if (texture != null && texture.material != null)
        {
            var mat = texture.material;
            mat.SetFloat("_Mode", 3); // Set rendering mode to transparent
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    private void UpdateLivesText()
    {
        if (livesEnemyText != null)
        {
            livesEnemyText.text = $"Enemy lives: {livesEnemy}";
        }
    }

    private void UpdateTransparency()
    {
        if (texture != null)
        {
            float alpha = (float)livesEnemy / maxLivesEnemy; // Calculate alpha based on remaining health
            color.a = Mathf.Clamp(alpha, 0, 1); // Ensure alpha stays within valid bounds
            texture.material.color = new Color(color.r, color.g, color.b, color.a);
        }
    }

    public void TakeDamage(int damage, bool applyBleed = false)
    {
        livesEnemy -= damage;
        UpdateLivesText();
        Debug.Log($"Enemy took {damage} damage. Remaining lives: {livesEnemy}");

        UpdateTransparency(); // Update transparency based on current health

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

        UpdateTransparency(); // Update transparency based on current health

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
