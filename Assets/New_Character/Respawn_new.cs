using UnityEngine;

public class Respawn_New : MonoBehaviour
{
    [Header("Configuración de Respawn")]
    [Tooltip("Punto de respawn del jugador.")]
    public Transform respawnPoint;

    [Header("Configuración de Capas")]
    [Tooltip("Nombre de la capa que activa respawn inmediato (ignorando inmunidad).")]
    public string abyssLayerName = "Abyss";

    private int _abyssLayerInt;

    void Start()
    {
        // Inicializar capa del abismo
        _abyssLayerInt = LayerMask.NameToLayer(abyssLayerName);
        if (_abyssLayerInt == -1)
        {
            Debug.LogError($"ERROR: Capa '{abyssLayerName}' no existe. Creala en Project Settings > Tags and Layers.", this);
        }

        // Verificar respawnPoint
        if (respawnPoint == null)
        {
            Debug.LogError("ERROR: Asigna un 'respawnPoint' en el Inspector.", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo procesar si es el jugador
        if (!other.CompareTag("Player")) return;

        // Debug detallado
        Debug.Log($"Respawn: Jugador '{other.name}' entró en trigger. Capa: {LayerMask.LayerToName(other.gameObject.layer)}");

        // Verificar si el respawnPoint está asignado
        if (respawnPoint == null)
        {
            Debug.LogWarning("AVISO: respawnPoint no asignado. No se puede respawnear.");
            return;
        }

        // Obtener PlayerHealth (si existe)
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        // Caso 1: Es el Abyss → Respawn IGNORANDO inmunidad
        if (other.gameObject.layer == _abyssLayerInt)
        {
            Debug.Log("Respawn FORZADO (Abyss detectado).");
            PerformRespawn(other);
            return;
        }

        // Caso 2: No es Abyss → Respawn SOLO si no es inmune
        if (playerHealth == null || !playerHealth.isImmune)
        {
            Debug.Log("Respawn: Jugador no inmune. Teletransportando...");
            PerformRespawn(other);
        }
        else
        {
            Debug.Log("Respawn: Jugador es inmune. No se respawneará.");
        }
    }

    private void PerformRespawn(Collider playerCollider)
    {
        // Manejar CharacterController (si existe)
        CharacterController controller = playerCollider.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            playerCollider.transform.position = respawnPoint.position;
            controller.enabled = true;
        }
        else
        {
            playerCollider.transform.position = respawnPoint.position;
        }

        Debug.Log($"Respawn: Jugador movido a {respawnPoint.position}");
    }
}