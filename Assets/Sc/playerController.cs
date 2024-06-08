using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(touchingDirections))]
public class playerController : MonoBehaviour
{
    touchingDirections touchingDirections;
Vector2 moveInput;

[SerializeField]
private bool _isMoving = false;

public float walkSpeed = 5f;
public float runSpeed= 10f;
Rigidbody2D rb;

public LayerMask groundLayer;

public Transform wallCheck;

bool isWallTouch;
bool isSliding;

public float wallSlidingSpeed;

float h;
bool isGrounded;


private void Update ()
{h=Input.GetAxisRaw("Horizontal");
    isWallTouch=Physics2D.OverlapBox(wallCheck.position,new Vector2 (0.5f,1.7f),0,groundLayer);
    if(isWallTouch && !isGrounded && h!=0)
{
    isSliding= true;
}else{isSliding=false;}
}




public float CurrentMoveSpeed
{get
{
    if (CanMove)
    {
        if(touchingDirections.IsGrounded)
        {
    
        }
        if (IsMoving && !touchingDirections.IsOnWall)
        {
            if(touchingDirections.IsGrounded )
            {
                if(IsRun){

                    return runSpeed;
                }else{
                    return walkSpeed;
                }
            }
            else{return airWalkSpeed;}
        }else{

            return 0;
        }
    }
    else
    {//no movement
        return 0;
    }


}
}
Animator animator;

    public bool IsMoving { get
    {
return _isMoving;
    } 
    private set
    {
    _isMoving = value; 
       animator.SetBool("isMoving",value);
    } 
    }
[SerializeField]
    private bool _isRun=false;
    public bool IsRun { get
    {return _isRun;}
    set
    {_isRun = value;
    animator.SetBool("isRun",value);}
    }
    public bool _isFacingRight=true;
    public float jumpImpulse = 10f;
    public float airWalkSpeed= 3f;

    public bool IsFacingRight { get{return _isFacingRight;} private set{
        
        if (_isFacingRight!=value)
    {
transform.localScale*=new Vector2(-1,1);
    }
    
    _isFacingRight= value;
    
    } }


    private void Awake()
{
  rb = GetComponent<Rigidbody2D>();
  animator=GetComponent<Animator>();
  touchingDirections=GetComponent<touchingDirections>();
}
  

     private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x*CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity,rb.velocity.y);

        if(isSliding)
        {rb.velocity= new Vector2(rb.velocity.x,Mathf.Clamp(rb.velocity.y,-wallSlidingSpeed,float.MaxValue));}

       
    }

   public void OnMove(InputAction.CallbackContext context)

    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving= moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
        
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x>0 && !IsFacingRight)
        {
            IsFacingRight=true;
        }
        else if (moveInput.x<0 && IsFacingRight)
        {
            IsFacingRight=false;
            }
    }

    public void OnRun(InputAction.CallbackContext context)

    {
       if(context.started)
       {
        IsRun= true;

       }
       else if(context.canceled)
{
    IsRun= false;
}    }

public void OnJump (InputAction.CallbackContext context)

{
if(context.started&& touchingDirections.IsGrounded&&CanMove)
{
animator.SetTrigger(AnimationStrings.jumpTrigger);
rb.velocity=new Vector2(rb.velocity.x,jumpImpulse);
}
}


public void OnAttack(InputAction.CallbackContext context)
{
if (context.started)
{
    animator.SetTrigger(AnimationStrings.attackTrigger);
} 

}

public bool CanMove
{
    get { return animator.GetBool(AnimationStrings.canMove); }
}

public bool IsAlive
{
    get { return animator.GetBool(AnimationStrings.isAlive); }
}
}
