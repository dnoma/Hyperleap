using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to Mario
    public Vector3 offset = new Vector3(0, 2, -10); // Offset from player

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            var newPositon = player.position + offset;

            newPositon.y = 0;

            transform.position = newPositon;
        }
    }
}