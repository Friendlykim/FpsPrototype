using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyRun = KeyCode.LeftShift;
    //[SerializeField]
    //private KeyCode keyJump = KeyCode.Space;
    
    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip audioClipWalk;
    [SerializeField]
    private AudioClip audioClipRun;
    
    private CameraControl rotateToMouse;
    private PlayerMovement movement;
    private PlayerStatus status;
    private PlayerAnimController Panimator;
    private AudioSource audioSource;
    private AkAssultRifle weapon;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotateToMouse = GetComponent<CameraControl>();
        movement = GetComponent<PlayerMovement>();
        status = GetComponent<PlayerStatus>();
        Panimator = GetComponent<PlayerAnimController>();
        audioSource = GetComponent<AudioSource>();
        weapon = GetComponentInChildren<AkAssultRifle>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotate();

        UpdateMoveTo();

        UpdateWeaponAction();
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
        //Debug.Log(x);
        if(x!=0||z!=0)
        {
            bool isRun = false;

            if (z > 0) isRun = Input.GetKey(keyRun);

            movement.MSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;
            Panimator.Movespeed = isRun == true ? 1 : 0.5f;
            audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

            if(audioSource.isPlaying == false)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            movement.MSpeed = 0;
            Panimator.Movespeed = 0;

            if(audioSource.isPlaying==true)
            {
                audioSource.Stop();
            }
        }
        movement.MoveTo(new Vector3(x, 0, z));

    }

    private void UpdateWeaponAction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weapon.StartAttack();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopAttack();
        }
    }
}
