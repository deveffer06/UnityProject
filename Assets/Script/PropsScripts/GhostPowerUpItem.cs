using UnityEngine;

public class GhostPowerUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GhostPowerUp ghost = other.GetComponentInChildren<GhostPowerUp>();

        if (ghost != null)
        {
            ghost.ActivateGhostPowerUp();
            Destroy(gameObject); 
        }
    }
}

