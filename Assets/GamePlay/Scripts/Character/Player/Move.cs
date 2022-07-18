using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private CharacterController characterController;
    public Vector3 velocity;
    public float gravity;
    public bool isGrounded;
    public float groundDistance;
    public LayerMask groundLayer;
    [SerializeField] private Animator animator;
    public GameObject raycastPoint;

    void Update()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            PlayerMove();
            PlayerRotate();
            if (!animator.GetBool(StringCache.IsRunning))
            {
                animator.SetBool(StringCache.IsRunning, true);
            }
        }
        else
        {
            if (animator.GetBool(StringCache.IsRunning)){
                animator.SetBool(StringCache.IsRunning, false);
            }
        }
    }

    private void PlayerMove()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);

        if(isGrounded && !raycastPoint.activeInHierarchy)
        {
            raycastPoint.SetActive(true);
        }

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        characterController.Move(new Vector3(joystick.Horizontal * moveSpeed, 0, joystick.Vertical * moveSpeed));
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void PlayerRotate()
    {
        float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, -angle + 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.8f);
    }
}
