using UnityEngine;

public class Fight : MonoBehaviour
{
    [SerializeField] private GameObject sword;

    private bool canAttack = true;

    private Animator animator;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack() 
    {
        if (InputManager.Instance.AttackAction.triggered && controller.isGrounded && canAttack)
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("Attack");
        }
    }
   

    public void StartAttack() 
    {
        canAttack = false;
    }

    public void EndAttack() 
    {
        canAttack = true;
    }
    public void EnableDealDmg() 
    {
        var collider = sword.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
            collider.isTrigger = true;

        }
    }
    public void DisableDealDmg() 
    {
        var collider = sword.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
            collider.isTrigger = false;

        }
    }
    public void RootMotionEnd() 
    {
        animator.applyRootMotion=false;
    }
    public void AttackSound() { }

    public void DieAnimationStart()
    {
       
        DisableDealDmg();
    }
}
