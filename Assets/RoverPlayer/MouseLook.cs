using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float rotationx = 0f;
    public float rotationy = 0f;
    public Transform turret;
    public int camzoom = 0;
    public float sensitivity = 15f;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotationy += Input.GetAxis("Mouse X") * sensitivity;
        rotationx += Input.GetAxis("Mouse Y") * -1 * sensitivity;
        rotationx = Mathf.Clamp(rotationx, -90f, 5f);
        transform.localEulerAngles = new Vector3(rotationx, rotationy, 0);
        turret.localEulerAngles = new Vector3(rotationx, rotationy, 0);
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (camzoom == 0)
            {
                Camera.main.fieldOfView = 20f;
                camzoom++;
            }
            else if(camzoom == 1)
            {
                Camera.main.fieldOfView = 6.5f;
                camzoom++;
            }
            else
            {
                Camera.main.fieldOfView = 60f;
                camzoom = 0;
            }
        }
    }
}
