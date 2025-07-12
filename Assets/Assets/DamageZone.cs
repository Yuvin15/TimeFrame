using UnityEngine;
using System.Collections;

public class DamageZone : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond = 5f;
    public float damageInterval = 1f; // How often to apply damage (in seconds)

    private PlayerHealth playerInZone;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerInZone = playerHealth;
            damageCoroutine = StartCoroutine(ApplyDamageOverTime());
            Debug.Log("Player entered damage zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            playerInZone = null;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            Debug.Log("Player left damage zone");
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        while (playerInZone != null)
        {
            playerInZone.TakeDamage(damagePerSecond);
            Debug.Log("Player took damage: " + damagePerSecond);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}