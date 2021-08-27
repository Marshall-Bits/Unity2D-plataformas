using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] float runSpeed = 10;
   [SerializeField] float jumpSpeed = 5f;
   [SerializeField] float climbSpeed = 3f;
   Rigidbody2D rigidbody;
   Collider2D collider;
   BoxCollider2D boxCollider;
   SpriteRenderer spriteRenderer;
   Animator animator;

   bool checkForGround;
   bool checkForLadders;
    // Start is called before the first frame update
    void Start()
    {
      rigidbody = gameObject.GetComponent<Rigidbody2D>();
      collider = gameObject.GetComponent<Collider2D>();
      boxCollider = gameObject.GetComponent<BoxCollider2D>();
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         Run();  
         Jump();
         Climb();
         FlipSprite();
         CheckForGround();
         CheckForLadders();
    }

    private void Run()
    {
       var getDirection = Input.GetAxis("Horizontal");
       rigidbody.velocity = new Vector2(getDirection * runSpeed, rigidbody.velocity.y);
       animator.SetBool("isRunning", true);
       if(getDirection == 0)
       {
         animator.SetBool("isRunning", false);
       }
    }
    
    private void Jump()
    {
       animator.SetFloat("jumpVelocity", rigidbody.velocity.y);
       if(!checkForGround){return;}
       if(Input.GetButton("Jump"))
       {
         rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
       }
    }

    private void Climb()
    {
       if(!checkForLadders)
       {
         boxCollider.isTrigger = false;
         rigidbody.gravityScale = 3;
         animator.SetBool("isClimbing",false);
         return;
       }
       var getDirection = Input.GetAxis("Vertical");
       if(checkForLadders && Input.GetAxis("Vertical") !=0 )
       {
         animator.SetBool("isClimbing",true);
         rigidbody.velocity = new Vector2(rigidbody.velocity.x, climbSpeed*getDirection);
         boxCollider.isTrigger = true;
         rigidbody.gravityScale = 0;
       }
      
    }

   private void FlipSprite()
   {
      if(rigidbody.velocity.x < 0)
      {
         spriteRenderer.flipX = true;
      }
      else if(rigidbody.velocity.x > 0)
      {
          spriteRenderer.flipX = false;
      }
   }

   private void CheckForGround()
   {
      if(collider.IsTouchingLayers(LayerMask.GetMask("ground")))
      {
         animator.SetBool("isGrounded",true);
         checkForGround = true;
      }
      else
      {
         animator.SetBool("isGrounded",false);
         checkForGround = false;
      }
   }

   private void CheckForLadders()
   {
      if(boxCollider.IsTouchingLayers(LayerMask.GetMask("ladders")))
      {
         checkForLadders = true;
         boxCollider.size = new Vector2 (1f, boxCollider.size.y);
      }
      else
      {
         checkForLadders = false;
         boxCollider.size = new Vector2 (0.65f, boxCollider.size.y);
      }
   }
}
