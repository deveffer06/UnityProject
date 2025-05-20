using System.Collections;
using UnityEngine;

public class GhostPowerUp: MonoBehaviour
{
    [Header("Power-Up Settings")]
    public float ghostDuration = 10f;
    public float ghostSpeed = 5f;
    
    private bool isGhost;
    private Rigidbody rb;
    private float fixedHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateGhostPowerUp()
    {
        if (!isGhost)
            StartCoroutine(GhostRoutine());
    }

    private IEnumerator GhostRoutine()
    {
        isGhost = true;
        fixedHeight = transform.position.y;
        
        float timer = 0f;
        while (timer < ghostDuration)
        {
            // Contrarrestar gravedad aplicando fuerza hacia arriba
            rb.AddForce(-Physics.gravity * rb.mass, ForceMode.Force);
            
            // Movimiento horizontal
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            
            Vector3 input = new Vector3(h, 0f, v);
            Vector3 move = transform.TransformDirection(input.normalized) * ghostSpeed;
            
            // Aplicar solo velocidad horizontal, mantener Y fija
            rb.linearVelocity = new Vector3(move.x, 0f, move.z);
            
            // Forzar altura si se desvía mucho
            if (Mathf.Abs(transform.position.y - fixedHeight) > 0.1f)
            {
                Vector3 pos = transform.position;
                pos.y = fixedHeight;
                transform.position = pos;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            }
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        rb.linearVelocity = Vector3.zero;
        isGhost = false;
    }
}