using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JetpackController jetpack = other.GetComponentInChildren<JetpackController>();
            if (jetpack != null)
            {
                jetpack.UseJetpack();
                
                // Destruir el prop
                Destroy(gameObject);
            }
        }
    }
}
