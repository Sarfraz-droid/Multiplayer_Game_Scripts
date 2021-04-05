using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerManager player;
    public float sensivity = .005f;
    public float clampangle = 85f;

    private float vertical_rotation;
    private float horizontal_rotation;

    private void Start()
    {

        vertical_rotation = transform.eulerAngles.x;
        horizontal_rotation = transform.eulerAngles.y;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursor();
        }
        if(Cursor.lockState == CursorLockMode.Locked)
            Look();
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }
    private void Look()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        vertical_rotation -= mouse_y * sensivity;
        horizontal_rotation += mouse_x * sensivity;

        vertical_rotation = Mathf.Clamp(vertical_rotation, -clampangle, clampangle);

        transform.localRotation = Quaternion.Euler(vertical_rotation, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, horizontal_rotation, 0f);
    }
    private void ToggleCursor()
    {
        Cursor.visible = !Cursor.visible;

        if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
