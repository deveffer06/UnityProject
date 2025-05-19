using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("COnfiguraci�n de la plataforma")]
    [Tooltip("Altura m�nima de la plataforma en unds del mundo")]
    [SerializeField] private float minHeight = 0f;
    [Tooltip("Altura m�xima de la plataforma en unds del mundo")]
    [SerializeField] private float maxHeight = 0f;
    [Tooltip("Velocidad de movimiento en unds / s")]
    [SerializeField] private float speed = 2f;
    private bool movingUp = true;
    private Vector3 initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        float currentHeight = transform.position.y;
        Vector3 movement = movingUp ? Vector3.up : Vector3.down;
        movement *= speed * Time.deltaTime;

        transform.Translate(movement);

        float maxY = initialPosition.y + maxHeight;
        float minY = initialPosition.y + minHeight;

        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            movingUp = false;
        }
        else if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            movingUp = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 minPoint = new Vector3(transform.position.x, transform.position.y + minHeight, transform.position.z);
        Vector3 maxPoint = new Vector3(transform.position.x, transform.position.y + maxHeight, transform.position.z);

        Gizmos.DrawLine(minPoint, maxPoint);
        Gizmos.DrawWireSphere(minPoint, 0.2f);
        Gizmos.DrawWireSphere(maxPoint, 0.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}