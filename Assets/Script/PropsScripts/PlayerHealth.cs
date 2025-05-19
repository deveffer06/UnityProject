using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Salud")]
    public float maxHealth = 100f;
    private float currentHealth;
    
    [Header("Configuración de Inmunidad")]
    public bool isImmune = false;
    
    [Tooltip("Material para mostrar cuando el jugador está inmune")]
    public Material immunityMaterial;
    private Material originalMaterial;
    
    [Tooltip("Renderer del personaje para cambiar material")]
    public Renderer playerRenderer;
    
    [Header("Efectos Visuales")]
    [Tooltip("Prefab de efecto para cuando inicia la inmunidad")]
    public GameObject immunityStartEffect;
    
    [Tooltip("Prefab de efecto para cuando termina la inmunidad")]
    public GameObject immunityEndEffect;
    
    private Coroutine immunityCoroutine;

    void Start()
    {
        // Inicializar la salud
        currentHealth = maxHealth;
        
        // Guardar el material original si tenemos un renderer
        if (playerRenderer != null)
        {
            originalMaterial = playerRenderer.material;
        }
    }

    // Función llamada cuando el jugador recibe daño
    public void TakeDamage(float damage)
    {
        // Si el jugador está inmune, no recibe daño
        if (isImmune)
        {
            Debug.Log("¡Inmune al daño!");
            return;
        }
        
        // Aplicar daño
        currentHealth -= damage;
        Debug.Log($"Salud actual: {currentHealth}");
        
        // Comprobar si el jugador ha muerto
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Función para activar la inmunidad
    public void ActivateImmunity(float duration)
    {
        // Si ya hay una coroutine de inmunidad, detenerla
        if (immunityCoroutine != null)
        {
            StopCoroutine(immunityCoroutine);
        }
        
        // Iniciar nueva coroutine de inmunidad
        immunityCoroutine = StartCoroutine(ImmunityRoutine(duration));
    }

    // Coroutine para manejar la inmunidad
    private IEnumerator ImmunityRoutine(float duration)
    {
        // Activar inmunidad
        isImmune = true;
        Debug.Log($"¡Inmunidad activada por {duration} segundos!");
        
        // Cambiar material si tenemos uno configurado
        if (playerRenderer != null && immunityMaterial != null)
        {
            playerRenderer.material = immunityMaterial;
        }
        
        // Instanciar efecto de inicio si existe
        if (immunityStartEffect != null)
        {
            Instantiate(immunityStartEffect, transform.position, Quaternion.identity);
        }
        
        // Esperar la duración
        yield return new WaitForSeconds(duration);
        
        // Desactivar inmunidad
        isImmune = false;
        Debug.Log("Inmunidad desactivada");
        
        // Restaurar material original
        if (playerRenderer != null && originalMaterial != null)
        {
            playerRenderer.material = originalMaterial;
        }
        
        // Instanciar efecto de fin si existe
        if (immunityEndEffect != null)
        {
            Instantiate(immunityEndEffect, transform.position, Quaternion.identity);
        }
        
        immunityCoroutine = null;
    }

    // Función llamada cuando el jugador muere
    private void Die()
    {
        Debug.Log("El jugador ha muerto");
    }
}
