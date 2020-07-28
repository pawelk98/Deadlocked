using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = Vector3.zero;
    public float moveSpeed = 5.0f;

    private Vector3 nextPostion = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            nextPostion = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, nextPostion, Time.deltaTime * moveSpeed);
        }
    }
}
