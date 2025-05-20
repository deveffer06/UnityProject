using UnityEngine;

public class ImmunityProp : MonoBehaviour
{
    [Header("Configuración")]
    public float immunityDuration = 10f;
    public GameObject pickupEffect;


    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ActivateImmunity(immunityDuration);

            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}