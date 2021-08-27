using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] float runSpeed = 10;
   [SerializeField] float jumpSpeed = 5;
   Rigidbody2D rigidbody;
   Collider2D collider;
   SpriteRenderer spriteRenderer;
   Animator animator;

   bool checkForGround;
    // Start is called before the first frame update
    void Start()
    {
      rigidbody = gameObject.GetComponent<Rigidbody2D>();
      collider = gameObject.GetComponent<Collider2D>();
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
       var getDirection = Input.GetAxis("Vertical");
       if(gameObject.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("ladders")))
      {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, getDirection * runSpeed);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        rigidbody.gravityScale = 0f;
      }
      else if (!collider.IsTouchingLayers(LayerMask.GetMask("ladders")))
      {
         rigidbody.gravityScale = 3f;
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
}
