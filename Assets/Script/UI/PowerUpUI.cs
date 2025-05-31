using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image healthBar;
    public Image jetpackFuelBar;
    public Image ghostDurationBar;
    public Image immunityDurationBar;
    
    [Header("Posicionamiento")]
    public float verticalOffset = 30f; // Espacio debajo de la barra de salud
    
    private JetpackController jetpack;
    private GhostPowerUp ghostPowerUp;
    private PlayerHealth playerHealth;
    
    void Start()
    {
        // Obtener referencias a los componentes necesarios
        jetpack = FindFirstObjectByType<JetpackController>();
        ghostPowerUp = FindFirstObjectByType<GhostPowerUp>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        
        // Posicionar las barras debajo de la barra de salud
        PositionBars();
        
        // Ocultar barras inicialmente
        ghostDurationBar.gameObject.SetActive(false);
        immunityDurationBar.gameObject.SetActive(false);
    }
    
    void Update()
    {
        // Actualizar barra de combustible del jetpack
        if (jetpack != null)
        {
            jetpackFuelBar.fillAmount = jetpack.currentFillRatio;
        }
        
        // Actualizar barra de duración del fantasma (si está activo)
        if (ghostPowerUp != null && ghostPowerUp.isGhost)
        {
            ghostDurationBar.gameObject.SetActive(true);
            // Aquí necesitarías una forma de acceder al tiempo restante del fantasma
            // Podrías modificar el script GhostPowerUp para exponer esta información
        }
        else
        {
            ghostDurationBar.gameObject.SetActive(false);
        }
        
        // Actualizar barra de duración de inmunidad (si está activa)
        if (playerHealth != null && playerHealth.isImmune)
        {
            immunityDurationBar.gameObject.SetActive(true);
            // Aquí necesitarías una forma de acceder al tiempo restante de inmunidad
            // Podrías modificar PlayerHealth para exponer esta información
        }
        else
        {
            immunityDurationBar.gameObject.SetActive(false);
        }
    }
    
    private void PositionBars()
    {
        RectTransform healthRect = healthBar.GetComponent<RectTransform>();
        Vector2 healthPos = healthRect.anchoredPosition;
        
        // Posicionar jetpack debajo de la salud
        jetpackFuelBar.GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(healthPos.x, healthPos.y - verticalOffset);
        
        // Posicionar ghost debajo de jetpack
        ghostDurationBar.GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(healthPos.x, healthPos.y - verticalOffset * 2);
        
        // Posicionar immunity debajo de ghost
        immunityDurationBar.GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(healthPos.x, healthPos.y - verticalOffset * 3);
    }
}