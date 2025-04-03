using Unity.Cinemachine;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [SerializeField] Transform reciever;
    [SerializeField] Transform grabPoint;

    Transform teleportObject = null;
    bool _Overlapping;

    Vector3 forward;

    private void Start()
    {
        forward = transform.parent.forward;
    }
    private void Update()
    {
        float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);

        if (_Overlapping)
        {
            Vector3 positionOffset = teleportObject.position - transform.position;
            float dot = Vector3.Dot(forward, positionOffset);

            if (dot < 0f)
            {
                rotationDiff += 180f;

                // Telport!!
                positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * positionOffset;
                teleportObject.position = reciever.position + positionOffset;

                Vector3 grabPointOffset = Quaternion.Euler(0f, rotationDiff, 0f) * (grabPoint.position - transform.position);
                grabPoint.position = reciever.position + grabPointOffset;
                _Overlapping = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            Debug.Log(other.transform);
            teleportObject = other.transform;
            // Make sure does not enter in the back of the portal
            Vector3 positionOffset = teleportObject.transform.position - transform.position;
            float dot = Vector3.Dot(forward, positionOffset);
            _Overlapping = dot > 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            _Overlapping = false;
        }
    }
}
