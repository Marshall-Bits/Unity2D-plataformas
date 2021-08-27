using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
   [SerializeField] float runSpeed = 10;
   [SerializeField] float jumpSpeed = 5;
   Rigidbody2D rigidbody;
   Collider2D collider;
   SpriteRenderer spriteRenderer;
   Animator animator;
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
         FlipSprite();
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
       if(!collider.IsTouchingLayers(LayerMask.GetMask("ground"))){return;}
       if(Input.GetButton("Jump"))
       {
         rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
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
}
