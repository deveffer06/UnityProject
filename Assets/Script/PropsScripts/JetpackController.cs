using UnityEngine;
using UnityEngine.UI;

public class JetpackController : MonoBehaviour
{
    private New_CharacterController characterController;

    [Header("Jetpack Settings")]
    public float jetpackAcceleration = 25f;
    [Range(0f, 1f)]
    public float jetpackDownwardVelocityCancelingFactor = 0.75f;
    public float consumeDuration = 1.5f;
    public float refillDurationGrounded = 2f;
    public float refillDurationTheAir = 5f;
    public float refillDelay = 1f;

    [Header("UI Settings")]
    public Image fuelBar; // Asignar desde el inspector

    public float currentFillRatio = 1f;
    private float lastTimeOfUse;
    private bool hasJetpackLastFrame; // Para detectar cambios

    void Start()
    {
        characterController = GetComponent<New_CharacterController>();
        if (fuelBar != null)
        {
            fuelBar.fillAmount = currentFillRatio;
            fuelBar.gameObject.SetActive(false); // Inicialmente oculta
        }
        hasJetpackLastFrame = characterController.hasJetpack;
    }

    void Update()
    {
        // Mostrar/ocultar barra cuando se obtiene/pierde el jetpack
        if (characterController.hasJetpack != hasJetpackLastFrame)
        {
            if (fuelBar != null)
            {
                fuelBar.gameObject.SetActive(characterController.hasJetpack);
                if (characterController.hasJetpack)
                {
                    fuelBar.fillAmount = currentFillRatio;
                }
            }
            hasJetpackLastFrame = characterController.hasJetpack;
        }

        if (!characterController.hasJetpack)
            return;

        UseJetpack();
        UpdateFuelUI();
    }

    public void UseJetpack()
    {
        bool jetpackIsInUse = characterController.hasJetpack &&
            currentFillRatio > 0f && Input.GetKey(KeyCode.Space);

        if (jetpackIsInUse)
        {
            lastTimeOfUse = Time.time;

            float totalAcceleration = jetpackAcceleration;
            totalAcceleration += Mathf.Abs(characterController.gravity);

            if (characterController.velocity.y < 0f)
            {
                totalAcceleration += ((-characterController.velocity.y / Time.deltaTime) * jetpackDownwardVelocityCancelingFactor);
            }

            Vector3 modifiedVelocity = characterController.velocity;
            modifiedVelocity.y += totalAcceleration * Time.deltaTime;
            characterController.velocity = modifiedVelocity;

            currentFillRatio -= Time.deltaTime / consumeDuration;
        }
        else
        {
            if (Time.time - lastTimeOfUse >= refillDelay)
            {
                float refillRate = 1f / (characterController.IsGrounded ? refillDurationGrounded : refillDurationTheAir);
                currentFillRatio += Time.deltaTime * refillRate;
            }

            currentFillRatio = Mathf.Clamp01(currentFillRatio);
        }
    }

    private void UpdateFuelUI()
    {
        if (fuelBar != null)
        {
            fuelBar.fillAmount = currentFillRatio;
        }
    }
}