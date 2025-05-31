using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GhostPowerUp : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public float ghostDuration = 10f;
    public float ghostSpeed = 5f;
    
    [Header("UI Settings")]
    public Image durationBar; // Asignar desde el inspector
    
    public bool isGhost;
    private Rigidbody rb;
    private float fixedHeight;
    private float currentDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (durationBar != null) durationBar.gameObject.SetActive(false);
    }

    public void ActivateGhostPowerUp()
    {
        if (!isGhost)
            StartCoroutine(GhostRoutine());
    }

    private IEnumerator GhostRoutine()
    {
        isGhost = true;
        fixedHeight = 0f;
        currentDuration = ghostDuration;
        
        if (durationBar != null)
        {
            durationBar.gameObject.SetActive(true);
            durationBar.fillAmount = 1f;
        }
        
        while (currentDuration > 0f)
        {
            // Contrarrestar gravedad
            rb.AddForce(-Physics.gravity * rb.mass, ForceMode.Force);
            
            // Movimiento horizontal
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            
            Vector3 input = new Vector3(h, 0f, v);
            Vector3 move = transform.TransformDirection(input.normalized) * ghostSpeed;
            
            rb.linearVelocity = new Vector3(move.x, 0f, move.z);
            
            // Forzar altura si es necesario
            if (Mathf.Abs(transform.position.y - fixedHeight) > 0.1f)
            {
                Vector3 pos = transform.position;
                pos.y = fixedHeight;
                transform.position = pos;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            }
            
            currentDuration -= Time.deltaTime;
            
            // Actualizar UI
            if (durationBar != null)
                durationBar.fillAmount = currentDuration / ghostDuration;
            
            yield return null;
        }
        
        rb.linearVelocity = Vector3.zero;
        isGhost = false;
        
        if (durationBar != null)
            durationBar.gameObject.SetActive(false);
    }
}