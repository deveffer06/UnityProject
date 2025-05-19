using UnityEngine;

public class FloatingPropEffect : MonoBehaviour
{
    public float rotationSpeed = 30f;       
    public float floatAmplitude = 0.1f;     
    public float floatFrequency = 5f;      

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
       
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);


        float newY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}