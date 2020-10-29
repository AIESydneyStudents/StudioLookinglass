using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform mainCamera;

    public string forwardAxis;
    public string sideAxis;

    public string movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(forwardAxis) > 0)
        {
            transform.rotation = mainCamera.rotation;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else if (Input.GetAxis(forwardAxis) < 0)
        {
            transform.rotation = mainCamera.rotation;
            transform.Rotate(0, 180, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        if (Input.GetAxis(sideAxis) > 0)
        {
            transform.rotation = mainCamera.rotation;
            transform.Rotate(0, 90, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else if (Input.GetAxis(sideAxis) < 0)
        {
            transform.rotation = mainCamera.rotation;
            transform.Rotate(0, -90, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}
