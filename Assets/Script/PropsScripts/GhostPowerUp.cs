using System.Collections;
using UnityEngine;

public class GhostPowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public float ghostDuration = 5f;
    public Material ghostMaterial;
    private Material originalMaterial;

    private bool isGhost = false;

    private Rigidbody rb;
    private Renderer playerRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        originalMaterial = playerRenderer.material;
    }

    public void ActivateGhostPowerUp()
    {
        if (!isGhost)
            StartCoroutine(GhostRoutine());
    }

    private IEnumerator GhostRoutine()
    {
        isGhost = true;

        // Cambiar apariencia
        playerRenderer.material = ghostMaterial;

        // Quitar gravedad
        rb.useGravity = false;

        // Congelar caída (si está cayendo, lo detiene)
        rb.linearVelocity = Vector3.zero;

        // Esperar tiempo
        yield return new WaitForSeconds(ghostDuration);

        // Revertir todo
        rb.useGravity = true;
        playerRenderer.material = originalMaterial;

        isGhost = false;
    }
}