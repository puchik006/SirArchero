using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public JoystickController joystick; // Reference to the on-screen joystick

    private float _borderX;
    private float _borderZ;


    private void Start()
    {
        _borderX = Camera.main.orthographicSize / 2;
        _borderZ = Camera.main.orthographicSize * 2;
    }

    private void Update()
    {
        // Input from the joystick
        float horizontalInput = joystick.GetInputVector().x;
        float verticalInput = joystick.GetInputVector().y;

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Movement
        MovePlayer(direction);
    }

    private void MovePlayer(Vector3 direction)
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Clamp the player's position within the game field bounds
        newPosition.x = Mathf.Clamp(newPosition.x, -_borderX, _borderX);
        newPosition.z = Mathf.Clamp(newPosition.z, -_borderZ, _borderZ);

        transform.position = newPosition;
    }
}
