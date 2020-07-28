using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Rigidbody playerRb;
    public float offset;

    void Update()
    {
        if(playerRb != null)
        {
            transform.position = new Vector3 (playerRb.position.x, playerRb.position.y + offset, playerRb.position.z);
        }
    }
}
