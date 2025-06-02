using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < threshold)
        {
            transform.position = new Vector3(-3.0259f, -1.280905f, -11.35335f);
        }
    }
}
