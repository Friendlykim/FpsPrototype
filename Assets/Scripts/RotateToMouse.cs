using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyRun = KeyCode.LeftShift;


    private CameraControl rotateToMouse;
    private PlayerMovement movement;
    private PlayerStatus status;
    private PlayerAnimController Panimator;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotateToMouse = GetComponent<CameraControl>();
        movement = GetComponent<PlayerMovement>();
        status = GetComponent<PlayerStatus>();
        Panimator = GetComponent<PlayerAnimController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotate();

        UpdateMoveTo();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMoveTo()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(x!=0||z!=0)
        {
            bool isRun = false;

            if (z > 0) isRun = Input.GetKey(keyRun);
            
                movement.MSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
                Panimator.Movespeed = isRun == true ? 1 : 0.5f;
            

        }
        else
        {
            movement.MSpeed = 0;
            Panimator.Movespeed = 0;
        }
        movement.MoveTo(new Vector3(x, 0, z));

    }
}
