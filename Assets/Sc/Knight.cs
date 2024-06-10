using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(touchingDirections),typeof(Damagable))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3F;

    public DetectionZone attacackZone;
    public DetectionZone cliffDetectionZone;
    private Rigidbody2D rb;
    touchingDirections touchingDirections;
     Animator animator;
      Damagable damageable;
    public  enum WalkableDirection
    {
        Right,Left
    }

    private WalkableDirection _walkDirection;

    private Vector2 walkDirectionVector=Vector2.right;

    public float walkStopRate = 0.6f;    
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection!=value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y );
                if (value==WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector=Vector2.left; 
                }
            }
            
            _walkDirection = value;
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<touchingDirections>();

        animator = GetComponent<Animator>();
        damageable = GetComponent<Damagable>();
    }
    void Update()
    {
        HasTarget = attacackZone.detectedColliders.Count > 0;

        if (AttackCooldown>0)
        {
            AttackCooldown -= Time.deltaTime;

        }
    }

    public float AttackCooldown {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCooldown,Mathf.Max(value,0));
        } }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget;}
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget,value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)

                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,0,walkStopRate), rb.velocity.y);
        }
       
    }

    private void FlipDirection()
    {
        if (WalkDirection==WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
            
        }
        else
        {
            Debug.LogError("splesh");
        }
            
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        //LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }
   public bool LockVelocity
    {
        get
        {
            return  animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity,value);
        }
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

}
