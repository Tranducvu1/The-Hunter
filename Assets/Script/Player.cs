using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 20f;
    public float playerSprint = 3f;

    [Header("Player Animator and Gravity")]
    public CharacterController characterController;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Script Camera")]
    public Transform playerCamera;

    [Header("Player Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    public float turnCalmVelocity;
    public float jumpRange = 1f;
    public Vector3 velocity;
    public Transform surfaceCheck;
    public bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask layerMask;

    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
      
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, layerMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        HandleMovement();
        Jump();
        Sprint();
    }

    void HandleMovement()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        bool newIsMoving = direction.magnitude >= 0.1f;

        if (newIsMoving != isMoving)
        {
            isMoving = newIsMoving;
            animator.SetBool("Idle", !isMoving);
            animator.SetBool("Walk", isMoving);
        }

        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", isMoving == false);
            animator.ResetTrigger("Jump");
        }
    }

    void Sprint()
    {
        if (Input.GetButton("Sprint") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            characterController.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
        }
    }
}