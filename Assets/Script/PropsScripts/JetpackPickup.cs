using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    public GameObject jetpackPrefab; // Jetpack visual
    public GameObject pickupParticlesPrefab; // Partículas al recoger
    public Transform jetpackHolder; // Lugar donde se instancia el jetpack visual
    private bool pickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;

        if (other.CompareTag("Player"))
        {
            New_CharacterController playerController = other.GetComponent<New_CharacterController>();
            if (playerController != null)
            {
                playerController.hasJetpack = true; // Activa el jetpack en el jugador
            }

            if (jetpackHolder == null)
            {
                Debug.LogError("JetpackHolder no ha sido asignado en el inspector.");
                return;
            }

            GameObject newJetpack = Instantiate(jetpackPrefab, jetpackHolder.position, jetpackHolder.rotation);
            newJetpack.transform.SetParent(jetpackHolder, worldPositionStays: true);

            if (pickupParticlesPrefab != null)
            {
                Instantiate(pickupParticlesPrefab, transform.position, Quaternion.identity);
            }

            pickedUp = true;

            Destroy(gameObject); // Elimina el pickup del mundo
        }
    }
}