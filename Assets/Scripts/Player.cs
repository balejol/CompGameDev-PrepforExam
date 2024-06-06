using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(BleedStatus))]
public class Player : MonoBehaviour, IDamageable
{
    [Header("Player state")]
    [SerializeField, Min(0)]
    private int livesPlayer = 3;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI livesPlayerText;
    [SerializeField]
    private GameObject gameOverPanel;

    private PlayerController playerController;
    private BleedStatus bleedStatus;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        bleedStatus = GetComponent<BleedStatus>();
    }

    private void Start()
    {
        UpdateLivesText();
        gameOverPanel.SetActive(false); // Ensure the Game Over panel is initially hidden
    }

    private void UpdateLivesText()
    {
        livesPlayerText.text = $"Lives: {livesPlayer}";
    }

    public void TakeDamage(int damage, bool applyBleed = false)
    {
        livesPlayer -= damage;
        UpdateLivesText();
        Debug.Log($"Player took {damage} damage. Remaining lives: {livesPlayer}");

        if (applyBleed)
        {
            Debug.Log("Player started bleeding!");
            bleedStatus.StartBleeding();
        }

        if (livesPlayer <= 0)
        {
            Debug.Log("Player has died!");
            StopGame();
        }
    }

    public void TakeBleedDamage(int damage)
    {
        livesPlayer -= damage;
        UpdateLivesText();
        Debug.Log($"Player took {damage} bleed damage. Remaining lives: {livesPlayer}");

        if (livesPlayer <= 0)
        {
            Debug.Log("Player has died from bleeding!");
            StopGame();
        }
    }

    private void StopGame()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        playerController.enabled = false;
        Debug.Log("Game Over!");
    }
}
