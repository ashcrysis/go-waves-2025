using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotateSpeed = 3f;
    [SerializeField] private float tiltAngle = 45f;

    private float yaw;

    void LateUpdate()
    {
        if (!target) return;

        if (Input.GetMouseButton(1))
            yaw += Input.GetAxis("Mouse X") * rotateSpeed;

        Quaternion yawRotation = Quaternion.Euler(0f, yaw, 0f);
        Vector3 offset = yawRotation * new Vector3(0f, 0f, -distance);

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y += Mathf.Tan(Mathf.Deg2Rad * tiltAngle) * distance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target.position);
        transform.rotation = Quaternion.Euler(tiltAngle, transform.eulerAngles.y, 0f);
    }
}