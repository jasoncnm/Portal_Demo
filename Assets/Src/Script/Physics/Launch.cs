using UnityEngine;

public class Launch : MonoBehaviour
{
    [SerializeField] float launchSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered Player");
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            Vector3 v = Vector3.Scale(rb.linearVelocity, Vector3.up);
            rb.linearVelocity -= v;
            rb.AddForce(Vector3.up * launchSpeed, ForceMode.Impulse);
        }
    }
}
