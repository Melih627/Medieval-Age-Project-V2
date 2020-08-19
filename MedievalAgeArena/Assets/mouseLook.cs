using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    float xRotation = 0f;
    float yRotation = 0f;
    public float SensitivityM = 500f;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX=Input.GetAxis("Mouse X")*Time.deltaTime* SensitivityM;
        float mouseY=Input.GetAxis("Mouse Y")*Time.deltaTime* SensitivityM;
        
        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        //Player.Rotate(Vector3.up * mouseX);







    }
}
