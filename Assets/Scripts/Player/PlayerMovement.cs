using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float torqueStrength = 50f;
    public float airControlMultiplier = 0.3f;

    [Header("Spin Control")]
    public float maxSpinAtSpeed = 8f;
    public float groundAngularDamping = 6f;
    public float airAngularDamping = 1f;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;

    Rigidbody rb;
    int groundContacts;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
    }

    void FixedUpdate()
    {
        // --- Input ---
        float h = Input.GetAxisRaw("Horizontal"); // A / D
        float v = Input.GetAxisRaw("Vertical");   // W / S

        // --- Sensitivity (from Options menu) ---
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);

        bool isGrounded = groundContacts > 0;
        float control = isGrounded ? 1f : airControlMultiplier;

        // --- Apply torque (sensitivity-aware) ---
        Vector3 torque =
            (Vector3.forward * -h + Vector3.right * v)
            * torqueStrength
            * sensitivity
            * control;

        rb.AddTorque(torque, ForceMode.Acceleration);

        // --- Dynamic angular damping ---
        rb.angularDamping = isGrounded ? groundAngularDamping : airAngularDamping;

        // --- Clamp spin relative to speed ---
        ClampSpinToSpeed();
    }

    void ClampSpinToSpeed()
    {
        float speed = rb.linearVelocity.magnitude;
        float maxAngular = speed * maxSpinAtSpeed;

        if (rb.angularVelocity.magnitude > maxAngular)
        {
            rb.angularVelocity =
                rb.angularVelocity.normalized * maxAngular;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
            groundContacts++;
    }

    void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
            groundContacts--;
    }
}
