using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public FloatingJoystick joystick; // Assign in inspector
    public Animator animator; // Assign PlayerMovement Animator
    private Rigidbody rb;

    private Vector3 moveDirection;
    public Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        target = GetComponent<ObjectDetectionRadius>().GetNearestGameobject();
        if (target != null)
        {
            // direction only on XZ plane
            Vector3 dir = target.position - transform.position;
            dir.y = 0f;

            // rotate player
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 10f);
        }
        MovementInput();
        HandleAnimations();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovementInput()
    {
        moveDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
    }

    private void MovePlayer()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            // Move using Rigidbody
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            //// Rotate in movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 0.1f));
        }
    }

    private void HandleAnimations()
    {
        bool isMoving = moveDirection.magnitude >= 0.1f;

        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isIdle", !isMoving);
        //animator.SetBool("isShooting", false); // trigger this separately
    }

    // Call this from your shooting script
    public void PlayShootAnimation()
    {
        animator.SetTrigger("shoot");
        //animator.SetBool("isShooting", true);
    }
}
