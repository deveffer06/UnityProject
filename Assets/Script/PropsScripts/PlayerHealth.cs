using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    
    [Header("Immunity Settings")]
    public bool isImmune;
    public float immunityDuration = 10f;
    
    [Header("UI Settings")]
    public Image immunityBar; // Asignar desde el inspector
    
    [Header("Visual Effects")]
    public Material immunityMaterial;
    private Material originalMaterial;
    public Renderer playerRenderer;
    public GameObject immunityStartEffect;
    public GameObject immunityEndEffect;
    
    private Coroutine immunityCoroutine;
    private float currentImmunityTime;

    void Start()
    {
        currentHealth = maxHealth;
        if (playerRenderer != null)
            originalMaterial = playerRenderer.material;
        
        if (immunityBar != null)
            immunityBar.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (isImmune)
        {
            Debug.Log("¡Inmune al daño!");
            return;
        }
        
        currentHealth -= damage;
        Debug.Log($"Salud actual: {currentHealth}");
        
        if (currentHealth <= 0)
            Die();
    }

    public void ActivateImmunity(float duration)
    {
        if (immunityCoroutine != null)
            StopCoroutine(immunityCoroutine);
        
        immunityCoroutine = StartCoroutine(ImmunityRoutine(duration));
    }

    private IEnumerator ImmunityRoutine(float duration)
    {
        isImmune = true;
        currentImmunityTime = duration;
        
        if (immunityBar != null)
        {
            immunityBar.gameObject.SetActive(true);
            immunityBar.fillAmount = 1f;
        }
        
        if (playerRenderer != null && immunityMaterial != null)
            playerRenderer.material = immunityMaterial;
        
        if (immunityStartEffect != null)
            Instantiate(immunityStartEffect, transform.position, Quaternion.identity);
        
        while (currentImmunityTime > 0f)
        {
            currentImmunityTime -= Time.deltaTime;
            
            if (immunityBar != null)
                immunityBar.fillAmount = currentImmunityTime / duration;
            
            yield return null;
        }
        
        isImmune = false;
        
        if (playerRenderer != null && originalMaterial != null)
            playerRenderer.material = originalMaterial;
        
        if (immunityEndEffect != null)
            Instantiate(immunityEndEffect, transform.position, Quaternion.identity);
        
        if (immunityBar != null)
            immunityBar.gameObject.SetActive(false);
        
        immunityCoroutine = null;
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto");
        // Aquí iría la lógica de muerte del jugador
    }
}