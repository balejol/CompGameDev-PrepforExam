using System.Collections;
using UnityEngine;

public class BleedStatus : MonoBehaviour
{
    [SerializeField, Min(0.1f)]
    private float damageInterval = 1f;  // Time between each bleed damage application
    [SerializeField, Min(1)]
    private int bleedDamage = 1;        // Damage per interval
    [SerializeField, Min(1)]
    private int bleedDuration = 5;      // Total duration of the bleed in seconds

    private IEnumerator bleedCoroutine;

    public void StartBleeding()
    {
        if (bleedCoroutine != null)
        {
            StopCoroutine(bleedCoroutine);
            Debug.Log("Stopped existing bleed coroutine.");
        }
        bleedCoroutine = ApplyBleed();
        StartCoroutine(bleedCoroutine);
        Debug.Log($"Started bleeding with interval {damageInterval}s, damage {bleedDamage}, duration {bleedDuration}s.");
    }

    public void StopBleeding()
    {
        if (bleedCoroutine != null)
        {
            StopCoroutine(bleedCoroutine);
            bleedCoroutine = null;
            Debug.Log("Stopped bleeding.");
        }
    }

    private IEnumerator ApplyBleed()
    {
        int ticks = bleedDuration;
        IDamageable damageable = GetComponent<IDamageable>();

        while (ticks > 0)
        {
            yield return new WaitForSeconds(damageInterval);
            Debug.Log($"Applying bleed damage: {bleedDamage}. Ticks remaining: {ticks - 1}");
            if (damageable != null)
            {
                damageable.TakeBleedDamage(bleedDamage);
            }
            else
            {
                Debug.LogError("IDamageable component not found!");
                break;
            }
            ticks--;
        }

        bleedCoroutine = null;
        Debug.Log("Bleeding effect ended.");
    }
}
