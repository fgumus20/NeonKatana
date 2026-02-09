using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.12f;

    [Header("Bounds")]
    [SerializeField] private BoxCollider levelBounds;
    [SerializeField] private float edgePadding = 0.5f;

    private Vector3 vel;

    private float fixedY;
    private Quaternion fixedRot;

    private void Awake()
    {
        fixedY = transform.position.y;
        fixedRot = transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null || levelBounds == null) return;

        Vector3 desired = target.position + offset;
        desired.y = fixedY;

        desired = ClampToBounds(desired);

        Vector3 next = Vector3.SmoothDamp(transform.position, desired, ref vel, smoothTime);

        transform.position = next;
        transform.rotation = fixedRot;
    }

    private Vector3 ClampToBounds(Vector3 camPos)
    {
        Bounds b = levelBounds.bounds;

        float minX = b.min.x + edgePadding;
        float maxX = b.max.x - edgePadding;
        float minZ = b.min.z + edgePadding;
        float maxZ = b.max.z - edgePadding;

        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.z = Mathf.Clamp(camPos.z, minZ, maxZ);

        return camPos;
    }
}
