using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] float runSpeed = 10;
   [SerializeField] float jumpSpeed = 5f;
   [SerializeField] float climbSpeed = 3f;
   Rigidbody2D playerRigidBody;
   Collider2D playerCollider;
   BoxCollider2D boxCollider;
   SpriteRenderer spriteRenderer;
   Animator animator;
   GameObject tryAgainButton;

   bool checkForGround;
   bool checkForLadders;
   bool playerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
      playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
      playerCollider = gameObject.GetComponent<Collider2D>();
      boxCollider = gameObject.GetComponent<BoxCollider2D>();
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      animator = gameObject.GetComponent<Animator>();
      tryAgainButton = GameObject.Find("TryAgainButton");
      tryAgainButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(playerAlive)
       {
         Run();  
         Jump();
         Climb();
         FlipSprite();
         CheckForGround();
         CheckForLadders();
         Die();
       }
    }

    private void Run()
    {
       var getDirection = Input.GetAxis("Horizontal");
       playerRigidBody.velocity = new Vector2(getDirection * runSpeed, playerRigidBody.velocity.y);
       animator.SetBool("isRunning", true);
       if(getDirection == 0)
       {
         animator.SetBool("isRunning", false);
       }
    }
    
    private void Jump()
    {
       animator.SetFloat("jumpVelocity", playerRigidBody.velocity.y);
       if(!checkForGround){return;}
       else if(Input.GetButton("Jump"))
       {
         playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpSpeed);
       }
    }

    private void Climb()
    {
       var getDirection = Input.GetAxis("Vertical");
       if(!checkForLadders)
       {
         boxCollider.isTrigger = false;
         playerRigidBody.gravityScale = 3;
         animator.SetBool("isClimbing",false);
         return;
       }
       else if(checkForLadders && Input.GetAxis("Vertical") !=0 )
       {
         animator.SetBool("isClimbing",true);
         playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, climbSpeed*getDirection);
         boxCollider.isTrigger = true;
         playerRigidBody.gravityScale = 0;
       }
      
    }

   private void FlipSprite()
   {
      if(playerRigidBody.velocity.x < 0)
      {
         spriteRenderer.flipX = true;
      }
      else if(playerRigidBody.velocity.x > 0)
      {
          spriteRenderer.flipX = false;
      }
   }

   private void CheckForGround()
   {
      if(playerCollider.IsTouchingLayers(LayerMask.GetMask("ground")))
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
   private void Die()
   {
      if(playerCollider.IsTouchingLayers(LayerMask.GetMask("enemy")) || playerCollider.IsTouchingLayers(LayerMask.GetMask("lava")))
      {
         animator.SetTrigger("dead");
         playerRigidBody.constraints = RigidbodyConstraints2D.None;
         playerAlive = false;
         tryAgainButton.SetActive(true);
      }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if(collision.gameObject.tag == "platform")
      {
         transform.parent = collision.transform;
      }
   }

   private void OnCollisionExit2D(Collision2D collision) 
   {
      if(collision.gameObject.tag == "platform")
      {
         transform.parent = null;
      } 
   }
}
