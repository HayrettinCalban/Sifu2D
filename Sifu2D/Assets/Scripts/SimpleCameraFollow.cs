using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public float cameraZ = -10f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, cameraZ);
            transform.position = newPosition;
        }
    }
}