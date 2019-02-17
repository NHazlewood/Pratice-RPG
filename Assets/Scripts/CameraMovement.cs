using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float camSpeed = 10f;
    public float zoomSpeed = 5f;
    public float panSpeed = 5f;

    Vector3 movement;
    Rigidbody cameraBody;
    Camera cam;

    void Awake()
    {
        cameraBody = GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Move(x,z);

        float y = Input.GetAxisRaw("Mouse ScrollWheel");
        Zoom(y);

        float rotation = Input.GetAxisRaw("Rotation");
        Rotate(rotation);
    }

    void Move(float x, float z)
    {
        float forward = transform.eulerAngles.y;
        float forwardRadian = (forward * Mathf.PI) / 180;

        float trueX, trueZ;
        while(forward < 0) { forward += 360; }
        forward = forward % 360f;

        trueX = Mathf.Cos(forwardRadian) * x + Mathf.Sin(forwardRadian) * z;
        if (forward > 0f && forward < 180f)
        {
            trueZ = Mathf.Sin(forwardRadian) * x * -1 + Mathf.Cos(forwardRadian) * z;
        }
        else
        {
            trueZ = Mathf.Abs(Mathf.Sin(forwardRadian)) * x + Mathf.Cos(forwardRadian) * z;
        }
        movement.Set(trueX, 0f, trueZ);
        movement = movement.normalized * camSpeed * Time.deltaTime;
        cameraBody.MovePosition(transform.position + movement);
    }

    void Zoom(float y)
    {
        cam.orthographicSize -= (y * zoomSpeed);
        if (cam.orthographicSize < 0.5f)
        {
            cam.orthographicSize = 0.5f;
        }
    }

    void Rotate(float rotation)
    {
        if (rotation != 0)
        {
            float newRotation = (rotation * panSpeed) + transform.eulerAngles.y;
            Quaternion target = Quaternion.Euler(60f, newRotation, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
        }
    }
}
