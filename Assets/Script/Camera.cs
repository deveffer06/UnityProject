using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera SetUp")]
    public Transform player;
    public Transform cameraTarget;
    public Vector3 shoulderOffset = new Vector3(0.3f, 1.7f, -2f);
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;
    public float mouseSensitivity = 0.5f;

    [Header("Orbita")]
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch;
    private New_CharacterController playerController;
    private Transform mainCamera;

    void Start()
    {
        playerController = player.GetComponent<New_CharacterController>();
        mainCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        HandleInput();
        UpdateCameraPostion();
    }

    void HandleInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (playerController.IsMoving)
        {
            yaw = playerController.CurrentYaw;
        }
        else
        {
            yaw += mouseX * rotationSpeed;
        }

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

    }

    void UpdateCameraPostion()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = cameraTarget.position + rotation * shoulderOffset;

        mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, followSpeed * Time.deltaTime);
        mainCamera.LookAt(cameraTarget);
    }
}
