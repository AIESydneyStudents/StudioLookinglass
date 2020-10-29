using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody body;

    public string forwardAxis;
    public string sideAxis;

    public float movementSpeed;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        Vector2 playerInput;
        playerInput.x = Input.GetAxis(sideAxis);
        playerInput.y = Input.GetAxis(forwardAxis);
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        // Move player
        Vector3 displacement = new Vector3(playerInput.x, 0, playerInput.y);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        displacement = rotation * displacement;
        transform.position += displacement * Time.deltaTime * movementSpeed;

        // Rotate to face direction of movement
        transform.LookAt(transform.position + displacement);

        //Force zero velocity
        body.velocity = Vector3.zero;
    }
}
