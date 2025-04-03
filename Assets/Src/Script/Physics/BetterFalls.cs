using UnityEngine;

public class BetterFalls : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    float fallMultiplier, lowJumpMultiplier;

    [SerializeField]
    float maxSpeed;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        // Custom gravity at different falling stages assuming downwards gravity
        if (rb.linearVelocity.y < -1 * maxSpeed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -1.0f * maxSpeed, rb.linearVelocity.z);
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * Time.deltaTime;
        } 
    }

}
