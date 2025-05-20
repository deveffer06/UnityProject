using UnityEngine;

public class Respawn_New : MonoBehaviour
{
    [Header("Referencia al punto de reapanw")]
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && respawnPoint != null)
        {
            if (other.CompareTag("Player") && respawnPoint != null)
            {
                // Verifica si tiene PlayerHealth y si es inmune
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null && playerHealth.isImmune)
                {
                    Debug.Log("Jugador es inmune, no se respawnea.");
                    return;
                }

                CharacterController controller = other.GetComponent<CharacterController>();
                if (controller != null)
                {
                    controller.enabled = false;
                    other.transform.position = respawnPoint.position;
                    controller.enabled = true;

                    Debug.Log("Jugador ha sido respawneado");
                }
                else
                {
                    other.transform.position = respawnPoint.position;
                    Debug.Log("Jugador sin characterController fue movido al respawn");
                }

            }
        }
    }
}