using UnityEngine;

public class JetpackController : MonoBehaviour
{
    public float maxFuel = 4f;
    public float thrustForce = 0.5f;
    public Rigidbody rigidBody;
    public Transform groundedTransform;
    public ParticleSystem effect;

    private float currentFuel;

    void Start()
    {
        currentFuel = maxFuel;
    }

    void Update()
    {
        // Puedes mantener este bloque si también quieres que funcione por input
        if (Input.GetAxis("Jump") > 0f)
        {
            UseJetpack();
        }
        else
        {
            TryRecharge();
        }
    }

    public void UseJetpack()
    {
        if (currentFuel > 0f)
        {
            currentFuel -= Time.deltaTime;
            rigidBody.AddForce(rigidBody.transform.up * thrustForce, ForceMode.Impulse);
            if (!effect.isPlaying) effect.Play();
        }
    }

    public void TryRecharge()
    {
        if (Physics.Raycast(groundedTransform.position, Vector3.down, 0.05f, LayerMask.GetMask("Grounded")) && currentFuel < maxFuel)
        {
            currentFuel += Time.deltaTime;
            if (effect.isPlaying) effect.Stop();
        }
        else
        {
            if (effect.isPlaying) effect.Stop();
        }
    }
}
