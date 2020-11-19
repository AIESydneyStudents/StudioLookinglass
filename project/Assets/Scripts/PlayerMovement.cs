using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody body;

    public string forwardAxis;
    public string sideAxis;

    public float maxVelocity;
    public float angle;
    [Range(0f,1f)]
    public float rotationSpeed;

    private Vector3 desiredVelocity;

    private Animator anim;

    [Range(0,1)] public float animationThreshold;
    public string animationName;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        desiredVelocity = new Vector3();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        Vector2 playerInput;
        playerInput.x = Input.GetAxis(sideAxis);
        playerInput.y = Input.GetAxis(forwardAxis);
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        anim.SetBool(animationName, playerInput.magnitude > animationThreshold);

        // Get velocity
        desiredVelocity = new Vector3(playerInput.x, 0, playerInput.y) * maxVelocity;
        // Rotate velocity for camera direction
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        desiredVelocity = rotation * desiredVelocity;

        // Rotate to face direction of movement
        Quaternion from = transform.rotation;
        transform.LookAt(transform.position + desiredVelocity);
        Quaternion to = transform.rotation;
        transform.rotation = Quaternion.Slerp(from, to, rotationSpeed);
    }

    private void FixedUpdate()
    {
        // Apply velocity to rigidbody
        body.velocity = desiredVelocity;

        // Handle height changes
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        rayOrigin.y += 1;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
