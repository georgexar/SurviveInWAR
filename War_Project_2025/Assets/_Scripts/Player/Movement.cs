using UnityEngine;

public class Movement : MonoBehaviour
{
  
    [Header("Speeds")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 5f;
    
    

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -25f;

    [Header("Rotation")]
    [SerializeField] private float rotationSmoothTime = 0.08f;
    

   
    // internals
    CharacterController controller;
    private Animator animator;
    private float targetSpeed = 0f;
    float verticalVelocity = 0f;
    private Vector2 moveInput;
    private bool runKey;
    private bool jumpKeyDown;

    private Vector2 currentAnimBlend;
    [SerializeField] private float animationSmoothTime = 0.1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();
    }

    private void HandleMovement()
    {
        if (animator.applyRootMotion) return;
        // 1) Input
        moveInput = InputManager.Instance.MoveAction.ReadValue<Vector2>();
        runKey = InputManager.Instance.RunAction.IsPressed();
        jumpKeyDown = InputManager.Instance.JumpAction.triggered;

        // 2) Camera forward/right (flattened)
        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        // 3) Movement direction relative to camera
        Vector3 inputDir = (camForward * moveInput.y + camRight * moveInput.x).normalized;

        // 4) Decide speed
        targetSpeed = 0f;
        if (moveInput != Vector2.zero)
        {
            targetSpeed = runKey ? runSpeed : walkSpeed;
        /*    if (moveInput.y > 0) targetSpeed = runKey ? runSpeed : walkSpeed;   // forward
            else if (moveInput.y < 0) targetSpeed = runKey ? runSpeed : walkSpeed; // backward
            else if (Mathf.Abs(moveInput.x) > 0) targetSpeed = runKey ? runSpeed : walkSpeed; // sidestep
        */
        }

        Vector3 horizontalVelocity = inputDir * targetSpeed;

        //Rotate the player towards camera.forward
        Quaternion targetRot = Quaternion.LookRotation(camForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSmoothTime);


       
        // 7) Jump & gravity
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = -1f;
            if (jumpKeyDown) verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -gravity);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // 8) Stop slide when no input
        if (moveInput == Vector2.zero) horizontalVelocity = Vector3.zero;

        // 9) Final move
        Vector3 finalVelocity = horizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(finalVelocity * Time.deltaTime);
    }

    private void HandleAnimations()
    {
        // Reset all movement states
        animator.SetBool("IsIdle", moveInput == Vector2.zero && controller.isGrounded);
        animator.SetBool("IsWalking", targetSpeed == walkSpeed && controller.isGrounded);
        animator.SetBool("IsRunning", targetSpeed == runSpeed && controller.isGrounded);
        animator.SetBool("IsJumpingUp", !controller.isGrounded && verticalVelocity > 0f);
        animator.SetBool("IsFalling", !controller.isGrounded && verticalVelocity < 0f);

       
        currentAnimBlend.x = Mathf.Lerp(currentAnimBlend.x, moveInput.x, animationSmoothTime);
        currentAnimBlend.y = Mathf.Lerp(currentAnimBlend.y, moveInput.y, animationSmoothTime);

        animator.SetFloat("MoveX", currentAnimBlend.x);
        animator.SetFloat("MoveY", currentAnimBlend.y);
    }

    public void RunStepSound() { }
    public void WalkStepSound() { }
    public void JumpUpSound() { }
}
