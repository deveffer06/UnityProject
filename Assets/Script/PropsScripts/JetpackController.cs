using UnityEngine;

public class JetpackController : MonoBehaviour
{
    private New_CharacterController characterController;

    public float jetpackAcceleration = 25f;
    [Range(0f, 1f)]
    public float jetpackDownwardVelocityCancelingFactor = 0.75f;
    public float consumeDuration = 1.5f;
    public float refillDurationGrounded = 2f;
    public float refillDurationTheAir = 5f;
    public float refillDelay = 1f;

    public float currentFillRatio = 1f;
    private float lastTimeOfUse;

    void Start()
    {
        characterController = GetComponent<New_CharacterController>();
    }

    void Update()
    {
        // Solo permitir usar jetpack si el jugador lo tiene
        if (!characterController.hasJetpack)
            return;

        UseJetpack();
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
}


