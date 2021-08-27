using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
   [SerializeField] float runSpeed = 10;
   [SerializeField] float jumpSpeed = 5;
   Rigidbody2D rigidbody;
   Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
      rigidbody = gameObject.GetComponent<Rigidbody2D>();
      collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         Run();  
         Jump();
    }

    private void Run()
    {
       var getDirection = Input.GetAxis("Horizontal");
       rigidbody.velocity = new Vector2(getDirection * runSpeed, rigidbody.velocity.y);
    }
    
    private void Jump()
    {
       if(Input.GetButton("Jump"))
       {
         rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
       }
    }

}
