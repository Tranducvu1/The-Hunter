using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;

    [Header("Player Animator and Gravity")]
    public CharacterController characterController;
    public float gravity = -9.81f;

    [Header("Player Script Camera")]
    public Transform playerCamera;

    [Header("Player Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public float jiumpRange = 1f;
    Vector3 velocity;
    bool onSerface;
    public float serfaceDistance = 0.4f;
    public LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis,0f,vertical_axis).normalized;

        if(direction.magnitude >= 0.1f )
        {
            //calculate the rotation angle that the character needs to face to face the direction of the movement
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            //smoothing to update pff objeect rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            //update object after smooothing
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //controll character

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(direction.normalized * playerSpeed *Time.deltaTime);
        }
    }
}
