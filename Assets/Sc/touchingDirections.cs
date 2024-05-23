using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class touchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance= 0.05f;
        public float wallDistance= 0.2f;

    public float ceilingDistance= 0.05f;

    Animator animator;
  CapsuleCollider2D touchingCollider;

  private Vector2 wallCheckDirection=>gameObject.transform.localScale.x>0? Vector2.right:Vector2.left;

  RaycastHit2D[] groundHits= new RaycastHit2D[5]; 
   RaycastHit2D[] wallHits= new RaycastHit2D[5];
     RaycastHit2D[] ceilingHits= new RaycastHit2D[5];


[SerializeField]
  private bool _isGrounded;
  [SerializeField]
    private bool _isOnWall;
    [SerializeField]
  private bool _isOnCeiling;

public bool IsOnWall { get{
        return _isOnWall;
    } private set{ 
        _isOnWall = value;
animator.SetBool(AnimationStrings.isOnWall,value);
    } }

    public bool IsOnCeiling { get{
        return _isOnCeiling;
    } private set{ 
        _isOnCeiling = value;
animator.SetBool(AnimationStrings.isOnCeiling,value);
    } }


    public bool IsGrounded { get{
        return _isGrounded;
    } private set{ 
        _isGrounded = value;
animator.SetBool(AnimationStrings._isGrounded,value);
    } }

    private void Awake()
  {
    touchingCollider=GetComponent<CapsuleCollider2D>();
    animator=GetComponent<Animator>();
  }
   
  
    void FixedUpdate()
    {
     IsGrounded=touchingCollider.Cast(Vector2.down,castFilter, groundHits,groundDistance)>0;
     IsOnWall= touchingCollider.Cast(wallCheckDirection,castFilter, wallHits,wallDistance)>0;
     IsOnCeiling=touchingCollider.Cast(Vector2.up,castFilter, ceilingHits,ceilingDistance)>0;
    }

   
}
