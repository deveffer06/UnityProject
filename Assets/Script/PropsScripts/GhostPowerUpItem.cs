using UnityEngine;

public class GhostPowerUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GhostPowerUp ghost = other.GetComponent<GhostPowerUp>();
        if (ghost != null)
        {
            ghost.ActivateGhostPowerUp();
            Destroy(gameObject); 
        }
    }
}

