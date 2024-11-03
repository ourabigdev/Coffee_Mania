using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 2f; // Adjust this to control the speed of movement
    public float maxHeight = 5f; // Adjust this to control maximum height
    public float minHeight = 2f; // Adjust this to control minimum height

    private float direction = 1; // 1 for up, -1 for down

    void Update()
    {
        // Move the cloud up or down
        transform.Translate(Vector3.up * speed * Time.deltaTime * direction);

        // Change direction if the cloud reaches max or min height
        if (transform.position.y >= maxHeight)
        {
            direction = -1; // Change direction to go down
        }
        else if (transform.position.y <= minHeight)
        {
            direction = 1; // Change direction to go up
        }
    }
}
