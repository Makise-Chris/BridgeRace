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
    private Rigidbody rb;
    [SerializeField] private Animator animator;

    void FixedUpdate()
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
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
    }

    private void PlayerRotate()
    {
        float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, -angle + 90, 0);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.8f);
    }
}
