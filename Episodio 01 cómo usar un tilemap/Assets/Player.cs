using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
  

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}
        Run();
        FlipSprite();
        Jump();
      
    }

    private void Run(){
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Running", playerHasHorizontalSpeed);
        float controlThrow = Input.GetAxis("Horizontal"); // value -1 -> 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
    }
    
    private void Jump(){
       
        if(Input.GetButtonDown("Jump")){
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpSpeed);
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void FlipSprite(){
        
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed){
            transform.localScale =  new Vector2 (Mathf.Sign(playerRigidBody.velocity.x) * transform.localScale.y, transform.localScale.y);
        }
    }
}
