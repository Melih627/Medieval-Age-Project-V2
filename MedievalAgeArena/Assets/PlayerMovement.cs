using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    Vector3 move;
    Vector3 velocity;
    public float speed=4f;
    float gravity = -9.81f;
    public float jumpHeight=2f;
    public LayerMask groundMask;
    public GameObject R_GroundCheck;
    public GameObject L_GroundCheck;
    public float GroundDistance=0.3f;
    bool R_isGround;
    bool L_isGround;
    
    private void Update()
    {
        CheckGround();
        Walk();
        Jump();
        Run();
        Attack();
        Gravity();


        
    }
   void Walk()
    {
        if (!animator.GetBool("Attack"))
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;
            if (move != Vector3.zero)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }

            controller.Move(move * speed * Time.deltaTime);
        }
    }
    void Jump()
    {
        if (velocity.y > -2f)
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && R_isGround && L_isGround)
        {
            
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }
    }
    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void CheckGround()
    {
        R_isGround = Physics.CheckSphere(R_GroundCheck.transform.position, GroundDistance, groundMask);
        L_isGround = Physics.CheckSphere(L_GroundCheck.transform.position, GroundDistance, groundMask);

        if (R_isGround && L_isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    }
    void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 2;
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && R_isGround && L_isGround)
        {
            if (!animator.GetBool("Attack"))
            {
                animator.SetBool("Attack", true);
                StartCoroutine(AttackRecoilRoutine());
            }
            
        }
        
    }
    IEnumerator AttackRecoilRoutine()
    {
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Attack", false);
    }
}
