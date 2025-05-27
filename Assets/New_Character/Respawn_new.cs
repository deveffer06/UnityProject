using UnityEngine;

public class Respawn_New : MonoBehaviour
{
    [Header("Referencia al punto de reapanw")]
    public Transform respawnPoint;

    [Header("Configuración de Capas")]
    [Tooltip("Nombre de la capa que representa el abismo/zona de muerte por caída.")]
    public string abyssLayerName = "Abyss"; // <-- Changed from "abismo" to "Abyss"

    private int abyssLayerInt;

    void Start()
    {
        abyssLayerInt = LayerMask.NameToLayer(abyssLayerName);
        if (abyssLayerInt == -1)
        {
            Debug.LogError($"Respawn_New: Layer '{abyssLayerName}' not found! Please ensure this layer exists in your Unity project.", this);
        }
        else
        {
            Debug.Log($"Respawn_New: '{abyssLayerName}' layer found with ID: {abyssLayerInt}");
        }

        if (respawnPoint == null)
        {
            Debug.LogError("Respawn_New: 'Respawn Point' is not assigned in the Inspector!", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Respawn_New: OnTriggerEnter called by {other.name}. Tag: {other.tag}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");

        // 1. Basic checks: Is it the Player and is respawnPoint assigned?
        if (!other.CompareTag("Player"))
        {
            Debug.Log($"Respawn_New: Object is not 'Player'. Tag: {other.tag}");
            return;
        }
        if (respawnPoint == null)
        {
            Debug.Log("Respawn_New: Respawn Point is NULL. Cannot respawn.");
            return;
        }

        Debug.Log("Respawn_New: Player tag and Respawn Point valid.");

        // Get the PlayerHealth component from the player
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogWarning("Respawn_New: PlayerHealth component not found on player. Cannot check immunity.");
        }
        else
        {
            Debug.Log($"Respawn_New: PlayerHealth found. isImmune: {playerHealth.isImmune}");
        }


        // 2. Check if the player has entered the Abyss layer
        if (other.gameObject.layer == abyssLayerInt)
        {
            Debug.Log("Respawn_New: Player entered the ABYSS LAYER. Initiating respawn (immunity ignored).");
            PerformRespawn(other);
            return; // Exit after handling abyss respawn
        }
        else
        {
            Debug.Log($"Respawn_New: Player entered a NON-ABYSS layer (Layer: {LayerMask.LayerToName(other.gameObject.layer)}).");
        }


        // 3. If NOT the Abyss layer, THEN check for immunity to prevent general respawn
        if (playerHealth != null && playerHealth.isImmune)
        {
            Debug.Log("Respawn_New: Player is immune, and this is NOT the abyss. Respawn averted.");
            return; // Stop execution, player is immune to non-abyss triggers (like spikes)
        }

        // 4. If reached here, it means:
        //    - It's the Player
        //    - respawnPoint is assigned
        //    - It's NOT the Abyss layer
        //    - Player is NOT immune (or PlayerHealth not found, in which case it proceeds)
        // This suggests it's a non-abyss trigger that *should* cause respawn/damage if not immune.
        // For THIS SCRIPT (Respawn_New), we only want to respawn for the abyss.
        // If other non-abyss triggers are calling this, it's likely a misconfiguration.
        Debug.Log("Respawn_New: Player hit a non-abyss trigger AND is not immune. This Respawn_New script should only handle Abyss respawn.");
    }

    // Helper method to perform the respawn logic
    private void PerformRespawn(Collider playerCollider)
    {
        Debug.Log($"Respawn_New: Attempting to respawn player to {respawnPoint.position}");
        CharacterController controller = playerCollider.GetComponent<CharacterController>();
        if (controller != null)
        {
            Debug.Log("Respawn_New: Found CharacterController. Disabling/Enabling for teleport.");
            controller.enabled = false;
            playerCollider.transform.position = respawnPoint.position;
            controller.enabled = true;
            Debug.Log("Respawn_New: Player has been respawned via CharacterController.");
        }
        else
        {
            Debug.Log("Respawn_New: No CharacterController found. Moving transform directly.");
            playerCollider.transform.position = respawnPoint.position;
            Debug.Log("Respawn_New: Player has been respawned via transform position.");
        }
    }
}