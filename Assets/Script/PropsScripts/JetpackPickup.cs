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
            playerController.hasJetpack = true;
        }

        if (jetpackHolder == null)
        {
            Debug.LogError("JetpackHolder no asignado en el inspector.");
            return;
        }

        // Instanciar el jetpack
        GameObject newJetpack = Instantiate(jetpackPrefab, jetpackHolder.position, jetpackHolder.rotation);
        newJetpack.transform.SetParent(jetpackHolder, worldPositionStays: false);
        newJetpack.transform.localPosition = Vector3.zero;
        newJetpack.transform.localRotation = Quaternion.identity;

        // ▶▶▶ Desactivar el FloatingPropEffect si existe ◀◀◀
        FloatingPropEffect floatingEffect = newJetpack.GetComponent<FloatingPropEffect>();
        if (floatingEffect != null)
        {
            floatingEffect.enabled = false; // Desactiva el script
            // O también: Destroy(floatingEffect); (si no lo necesitas más)
        }

        // Partículas de recogida (opcional)
        if (pickupParticlesPrefab != null)
        {
            Instantiate(pickupParticlesPrefab, transform.position, Quaternion.identity);
        }

        pickedUp = true;
        Destroy(gameObject); // Destruye el objeto recolectable
    }
}
}